using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using GradleBindings.Extensions;

namespace GradleBindings
{
    public class Gradle
    {
        public const string DefaultRepositores = 
@"repositories { 
    maven {
        url ""%M2LOCAL%"" //will be replaced by add-in
    }
    jcenter()
 }"; 

        private const string GradleScriptTemplate =

@"apply plugin: 'java'

def resolveDependencyString(String dependencyString) {
    def dependency = dependencies.create(dependencyString)
    configurations.detachedConfiguration(dependency).setTransitive(false).resolve()
}

def resolveDependencyStringTransitive(String dependencyString) {
    def dependency = dependencies.create(dependencyString)
    configurations.detachedConfiguration(dependency).setTransitive(true).resolve()
}

%CUSTOM_REPOS%

task getDeps(type: Copy) {
  def resultFileAll = new File(""%RESULT_FILE_ALL%"")
  def resultFileMain = new File(""%RESULT_FILE_MAIN%"")
  resolveDependencyString('%DEPENDENCY%').sort().each { resultFileMain.append('\n' + it.toString()) }
  resolveDependencyStringTransitive('%DEPENDENCY%').sort().each { resultFileAll.append('\n' + it.toString()) }
}";

        /// <summary>
        /// Extracts (resolves) list of dependencies (including transitive dependencies)
        /// </summary>
        /// <param name="dependency">Dependency id, i.e.  com.afollestad:material-dialogs:0.7.6.0</param>
        /// <param name="androidSdkHome">Path to Android SDK home, i.e.   C:\Users\Egorbo\AppData\Local\Android\sdk</param>
        /// <param name="customRepositories">By default it searches dependencies to resolve in the jcentral and the local M2 repositores but you can extended it</param>
        public static IEnumerable<DependencyFile> ExtractDependencies(string dependency, string androidSdkHome, string customRepositories = null)
        {
            dependency = dependency.Trim(' ', '\'', '\"');
            if (dependency.StartsWith("compile ")) //user copy-pasted compile 'dependency-id'
                dependency = dependency.Remove(0, 8).Trim(' ', '\'', '\\');

            string baseDirectory = Path.Combine(Path.GetTempPath(), "Xamarin.GradleBindings", Guid.NewGuid().ToString("N").Substring(0, 6));
            if (Directory.Exists(baseDirectory))
                Directory.Delete(baseDirectory, true);

            Directory.CreateDirectory(baseDirectory);

            var resultMainPath = Path.Combine(baseDirectory, "result_all.txt").FixPathForGradle();
            var resultAllPath = Path.Combine(baseDirectory, "result_main.txt").FixPathForGradle();

            var repositories = string.IsNullOrEmpty(customRepositories) ? DefaultRepositores : customRepositories;
            repositories = repositories.Replace("%M2LOCAL%", Path.Combine(androidSdkHome, @"extras\android\m2repository").FixPathForGradle() + "/");

            var script = GradleScriptTemplate
                .Replace("%CUSTOM_REPOS%", repositories)
                .Replace("%DEPENDENCY%", dependency)
                .Replace("%RESULT_FILE_ALL%", resultAllPath)
                .Replace("%RESULT_FILE_MAIN%", resultMainPath);


            CopyEmbeddedGradleFilesTo(baseDirectory);
            using (var sw = new StreamWriter(Path.Combine(baseDirectory, "build.gradle")))
            {
                sw.Write(script);
            }

            Environment.CurrentDirectory = baseDirectory;
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(baseDirectory, "gradlew.bat"),
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false, 
                    Arguments = "--info > log.txt"
                },
            };

            process.Start();
            process.WaitForExit();

            var log = File.ReadAllText(Path.Combine(baseDirectory, "log.txt"));

            if (!File.Exists(resultAllPath) ||
                !File.Exists(resultMainPath))
            {
                throw new GradleException(log, script);
            }

            var allDependencies = File.ReadAllLines(resultAllPath).Where(f => !String.IsNullOrWhiteSpace(f)).ToList(); //dependencies of the dependency
            var mainFiles = File.ReadAllLines(resultMainPath).Where(f => !String.IsNullOrWhiteSpace(f)).ToList();

            if (mainFiles.Count < 1)
            {
                throw new GradleException(log, script);
            }

            foreach (var file in mainFiles)
            {
                yield return new DependencyFile(file, false);
            }

            foreach (var file in allDependencies)
            {
                if (!mainFiles.Contains(file))
                    yield return new DependencyFile(file, true);
            }
        }

        private static void CopyEmbeddedGradleFilesTo(string destDir)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            const string gradleFileNamespace = "GradleBindings.GradleFiles";
            var gradleFiles = executingAssembly
                .GetManifestResourceNames()
                .Where(r => r.StartsWith(gradleFileNamespace));

            foreach (var gradleFile in gradleFiles)
            {
                var manifestResourceStream = executingAssembly.GetManifestResourceStream(gradleFile);
                var filePath = Path.Combine(destDir, gradleFile.Remove(0, gradleFileNamespace.Length + 1));
                using (Stream file = File.Create(filePath))
                {
                    manifestResourceStream.CopyTo(file);
                }
            }
        }
    }
}