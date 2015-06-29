using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using GradleBindings.Interfaces;

namespace GradleBindings
{
    public class GradleBindingsGenerator
    {
        private readonly IBusyIndicator _busyIndicator;
        private readonly ISettings _settings;
        private readonly IAndroidSdkDialog _androidSdkDialog;
        private readonly IBindingProjectGenerator _bindingProjectGenerator;
        private readonly IDependencyInputDialog _dependencyInputDialog;
        private readonly IDependencyOutputSelectorDialog _dependencyOutputSelectorDialog;
        private readonly IErrorDialog _errorDialog;

        public GradleBindingsGenerator(
            IBusyIndicator busyIndicator,
            ISettings settings,
            IAndroidSdkDialog androidSdkDialog,
            IBindingProjectGenerator bindingProjectGenerator,
            IDependencyInputDialog dependencyInputDialog, 
            IDependencyOutputSelectorDialog dependencyOutputSelectorDialog, 
            IErrorDialog errorDialog)
        {
            _busyIndicator = busyIndicator;
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

            var dependencyInput = await _dependencyInputDialog.ShowAsync(Gradle.DefaultRepositores);
            if (dependencyInput == null)
                return;

            try
            {
                _busyIndicator.IsBusy = true;
                var resultDependencies = Gradle.ExtractDependencies(dependencyInput.DependencyId, androidSdk, dependencyInput.DependencyRepository).ToList();
                _busyIndicator.IsBusy = false;
                if (resultDependencies.Count == 1 && !resultDependencies[0].IsTransitive)
                {
                    await _bindingProjectGenerator.GenerateAsync(sourceProjectName, dependencyInput.AssemblyName, resultDependencies);
                }
                else
                {
                    var filteredDependencies = await _dependencyOutputSelectorDialog.FilterDependenciesAsync(resultDependencies);
                    if (filteredDependencies != null)
                    {
                        await _bindingProjectGenerator.GenerateAsync(sourceProjectName, dependencyInput.AssemblyName, filteredDependencies);
                    }
                }
            }
            catch (GradleException exc)
            {
                _busyIndicator.IsBusy = false;
                _errorDialog.ShowError(exc.ToString());
            }
            catch (Exception exc)
            {
                _busyIndicator.IsBusy = false;
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
