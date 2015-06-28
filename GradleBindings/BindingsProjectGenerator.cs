namespace GradleBindings
{
    public class BindingsProjectGenerator
    {
        public void Generate(string solutionPath, string sourceProjectPath, string gradleFilePath, string directoryForBindingProjects)
        {
            //it's all done in "GradleBindings.VisualStudioPackage.cs" so far (via DTE api)
            //but I don't know how to do it for Xamarin Studio yet - does it support add-ins?
        }
    }
}
