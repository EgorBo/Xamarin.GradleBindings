using System;
using System.Linq;
using System.Threading.Tasks;
using GradleBindings.Interfaces;

namespace GradleBindings
{
    public class GradleBindingsGenerator
    {
        private readonly IBindingProjectGenerator _bindingProjectGenerator;
        private readonly IDependencyInputDialog _dependencyInputDialog;
        private readonly IDependencyOutputSelectorDialog _dependencyOutputSelectorDialog;
        private readonly IErrorDialog _errorDialog;

        public GradleBindingsGenerator(
            IBindingProjectGenerator bindingProjectGenerator,
            IDependencyInputDialog dependencyInputDialog, 
            IDependencyOutputSelectorDialog dependencyOutputSelectorDialog, 
            IErrorDialog errorDialog)
        {
            _bindingProjectGenerator = bindingProjectGenerator;
            _dependencyInputDialog = dependencyInputDialog;
            _dependencyOutputSelectorDialog = dependencyOutputSelectorDialog;
            _errorDialog = errorDialog;
        }

        public async Task Generate()
        {
            var dependencyInput = await _dependencyInputDialog.ShowAsync();
            if (dependencyInput == null)
                return;

            var androidSdk = await ResolveAndroidSdkPathAsync();
            if (androidSdk == null)
                return;

            try
            {
                var resultDependencies = Gradle.ExtractDependencies(dependencyInput.DependencyId, androidSdk, dependencyInput.DependencyRepository).ToList();
                if (resultDependencies.Count == 1 && !resultDependencies[0].IsDependency)
                {
                    await _bindingProjectGenerator.GenerateAsync(resultDependencies);
                }
                else
                {
                    var filteredDependencies =
                        await _dependencyOutputSelectorDialog.FilterDependenciesAsync(resultDependencies);
                    if (filteredDependencies != null)
                    {
                        await _bindingProjectGenerator.GenerateAsync(filteredDependencies);
                    }
                }
            }
            catch (GradleException exc)
            {
                
            }
            catch (Exception exc)
            {
                _errorDialog.ShowError(exc.Message);
                return;
            }
        }

        private async Task<string> ResolveAndroidSdkPathAsync()
        {
            //TODO: Xamarin VS add-in definitely knows where it is. find!
            var androidSdk = Environment.GetEnvironmentVariable("ANDROID_HOME");
            if (string.IsNullOrWhiteSpace(androidSdk))
            {
                _errorDialog.ShowError("%ANDROID_HOME% environment variable is not set.");
                return null;
            }
            return androidSdk;
        }
    }
}
