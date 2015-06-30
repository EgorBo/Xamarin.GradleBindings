using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GradleBindings.Interfaces;

namespace GradleBindings
{
    public class GradleBindingsGenerator
    {
        private readonly ISettings _settings;
        private readonly IAndroidSdkDialog _androidSdkDialog;
        private readonly IBindingProjectGenerator _bindingProjectGenerator;
        private readonly IDependencyInputDialog _dependencyInputDialog;
        private readonly IDependencyOutputSelectorDialog _dependencyOutputSelectorDialog;
        private readonly IErrorDialog _errorDialog;

        public GradleBindingsGenerator(
            ISettings settings,
            IAndroidSdkDialog androidSdkDialog,
            IBindingProjectGenerator bindingProjectGenerator,
            IDependencyInputDialog dependencyInputDialog, 
            IDependencyOutputSelectorDialog dependencyOutputSelectorDialog, 
            IErrorDialog errorDialog)
        {
            _settings = settings;
            _androidSdkDialog = androidSdkDialog;
            _bindingProjectGenerator = bindingProjectGenerator;
            _dependencyInputDialog = dependencyInputDialog;
            _dependencyOutputSelectorDialog = dependencyOutputSelectorDialog;
            _errorDialog = errorDialog;
        }

        public async Task Generate(string sourceProjectName)
        {
            var androidSdk = await ResolveAndroidSdkPathAsync();
            if (string.IsNullOrWhiteSpace(androidSdk))
                return;

            string workingDirectory = null;
            Exception exception = null;
            List<DependencyFile> resultDependencies = null;
            DependencyInputDialogResult dependencyInputResult = null;

            if (!await _dependencyInputDialog.ShowAsync(Gradle.DefaultRepositores,
                (dependencyInput) => {
                    dependencyInputResult = dependencyInput;
                    return Task.Factory.StartNew(() => {
                            try
                            {
                                resultDependencies = Gradle.ExtractDependencies(dependencyInput.DependencyId, androidSdk, out workingDirectory, dependencyInput.DependencyRepository).ToList();
                            }
                            catch (Exception exc)
                            {
                                exception = exc;
                            }
                        });
                }))
            {
                return;
            }

            if (exception != null)
            {
                _errorDialog.ShowError(exception.ToString());
                return;
            }

            try
            {
                string readMeContent = string.Format("Binding for '{0}'\nDependencies:\n\n{1}", 
                    dependencyInputResult.DependencyId, 
                    string.Join("\n", resultDependencies.Select(d => string.Format("{0},     Transitive={1},     Path={2}", Path.GetFileName(d.File), d.IsTransitive, d.File))));

                var bindingInfoFilePath = Path.Combine(workingDirectory, "GeneratedBindingInfo.txt");
                File.WriteAllText(bindingInfoFilePath, readMeContent);

                //if we have only one binary
                if (resultDependencies.Count == 1 && !resultDependencies[0].IsTransitive)
                {
                    await _bindingProjectGenerator.GenerateAsync(sourceProjectName, dependencyInputResult.AssemblyName, resultDependencies, bindingInfoFilePath);
                }
                else
                {
                    var filteredDependencies = await _dependencyOutputSelectorDialog.FilterDependenciesAsync(resultDependencies);
                    if (filteredDependencies != null && filteredDependencies.Any())
                    {
                        await _bindingProjectGenerator.GenerateAsync(sourceProjectName, dependencyInputResult.AssemblyName, filteredDependencies, bindingInfoFilePath);
                    }
                }
            }
            catch (Exception exc)
            {
                _errorDialog.ShowError(exc.ToString());
            }
        }

        private async Task<string> ResolveAndroidSdkPathAsync()
        {
            //TODO: Xamarin VS add-in definitely knows where it is. find!

            var androidSdk = Environment.GetEnvironmentVariable("ANDROID_HOME");
            if (string.IsNullOrWhiteSpace(androidSdk))
            {
                androidSdk = _settings.AndroidSdk;
                if (string.IsNullOrWhiteSpace(androidSdk))
                {
                    androidSdk = await _androidSdkDialog.AskAsync();
                    if (!string.IsNullOrWhiteSpace(androidSdk))
                    {
                        _settings.AndroidSdk = androidSdk;
                    }
                }
            }
            return androidSdk;
        }
    }
}
