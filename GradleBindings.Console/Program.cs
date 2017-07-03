using System;
using System.IO;
using System.Linq;

namespace GradleBindings.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync();

            System.Console.ReadKey();
        }

        private static async void MainAsync()
        {
            var androidSdk = SystemPaths.GetAndroidSdkPath();

            string repositoriesDir;
            if (!Gradle.HasLocalRepositories(androidSdk, out repositoriesDir))
                throw new Exception("Android SDK path is invalid or repositories are not installed via Android SDK Manager.");

            //Let's test: resolve all recommended dependencies!

            var deps = await new RecommendedDpendenciesService().GetAsync();
            var failed = deps.Where(dep => !ResolveAndPrintDependencies(dep.DependencyId, androidSdk)).ToList();
            System.Console.WriteLine("Done. Failed: {0}\n{1}", failed.Count, string.Join("\n", failed.Select(f => f.DependencyId)));
        }

        public static bool ResolveAndPrintDependencies(string dependencyId, string androidSdk)
        {
            try
            {
                System.Console.WriteLine("\nResolving '{0}'...", dependencyId);
                string workingDir;
                var result = Gradle.ExtractDependencies(dependencyId, androidSdk, out workingDir)
                    .OrderBy(f => f.IsTransitive)
                    .ToList();

                foreach (var file in result.OrderBy(f => f.IsTransitive))
                {
                    System.Console.WriteLine("{1}{0}", Path.GetFileName(file.File), file.IsTransitive ? "\t" : "");
                }

                return true;
            }
            catch (Exception)
            {
                System.Console.WriteLine("FAILED!");
                return false;
            }
        }
    }
}
