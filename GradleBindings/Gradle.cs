using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using GradleBindings.Extensions;

namespace GradleBindings
{
    public class Gradle
    {
        const string M2RepositoryRelativePath = @"extras\android\m2repository";

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
        /// <param name="detailedLog">if true, --info flag will be used</param>
        public static IEnumerable<DependencyFile> ExtractDependencies(string dependency, string androidSdkHome, out string workingDirectory, string customRepositories = null, bool detailedLog = false)
        {
            dependency = dependency.Trim();

            string baseDirectory = Path.Combine(Path.GetTempPath(), "Xamarin.GradleBindings", Guid.NewGuid().ToString("N").Substring(0, 6));
            if (Directory.Exists(baseDirectory))
                Directory.Delete(baseDirectory, true);

            Directory.CreateDirectory(baseDirectory);
            workingDirectory = baseDirectory;

            var resultMainPath = Path.Combine(baseDirectory, "result_all.txt").FixPathForGradle();
            var resultAllPath = Path.Combine(baseDirectory, "result_main.txt").FixPathForGradle();

            var repositories = string.IsNullOrEmpty(customRepositories) ? DefaultRepositores : customRepositories;
            repositories = repositories.Replace("%M2LOCAL%", Path.Combine(androidSdkHome, M2RepositoryRelativePath).FixPathForGradle() + "/");

            var script = GradleScriptTemplate
                .Replace("%CUSTOM_REPOS%", repositories)
                .Replace("%DEPENDENCY%", dependency)
                .Replace("%RESULT_FILE_ALL%", resultAllPath)
                .Replace("%RESULT_FILE_MAIN%", resultMainPath);

            CopyEmbeddedGradleFilesTo(baseDirectory);
            File.WriteAllText(Path.Combine(baseDirectory, "build.gradle"), script);

            var process = Process.Start(
                new ProcessStartInfo
                {
                    WorkingDirectory = baseDirectory,
                    FileName = Path.Combine(baseDirectory, "gradlew.bat"),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = detailedLog ? "--info" : ""
                });
            
            StringBuilder logBuilder = new StringBuilder();
            process.ErrorDataReceived += (sender, e) => logBuilder.AppendLine(e.Data);
            process.OutputDataReceived += (sender, e) => logBuilder.AppendLine(e.Data);
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            string log = logBuilder.ToString();
            
            
            if (!File.Exists(resultAllPath) ||
                !File.Exists(resultMainPath))
            {
                throw new GradleException(log, script);
            }

            var allDependencies = File.ReadAllLines(resultAllPath).Where(f => !String.IsNullOrWhiteSpace(f)).ToList(); //dependencies of the dependency
            var mainFiles = File.ReadAllLines(resultMainPath).Where(f => !String.IsNullOrWhiteSpace(f)).ToList();

            if (mainFiles.Count < 1)
            {
                throw new GradleException("No resolved dependencies were found\n" + log, script);
            }

            var result = new List<DependencyFile>();
            foreach (var file in mainFiles)
            {
                result.Add(new DependencyFile(file, false));
            }

            foreach (var file in allDependencies)
            {
                if (!mainFiles.Contains(file))
                    result.Add(new DependencyFile(file, true));
            }
            return result;
        }

        public static bool HasLocalRepositories(string androidSdkHome, out string repositoriesDir)
        {
            repositoriesDir = Path.Combine(androidSdkHome, M2RepositoryRelativePath);
            return Directory.Exists(repositoriesDir);
        }

        /// <summary>
        /// Only short form is supported
        /// </summary>
        public static bool ValidDependencyIdString(string id)
        {
            //TODO: improve pattern:
            return Regex.Match(id, @"^[^:]+:[^:]+:[^:]+$").Success;
        }

        /// <summary>
        /// com.afollestad:material-dialogs:0.7.6.0  -->  MaterialDialogs
        /// com.github.chrisbanes.photoview:library:+  -->  Photoview
        /// </summary>
        public static string GetReadableDependencyName(string dependencyId)
        {
            if (string.IsNullOrWhiteSpace(dependencyId))
                return string.Empty;

            try
            {
                dependencyId = dependencyId.Trim(' ', '\'', '\"');
                var parts = dependencyId.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 3)
                {
                    var rsb = new StringBuilder();
                    var name = parts[1];
                    if ("library".Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //meaningless name, let's use the last part of "group" instead:
                        name = parts[0];
                        if (!string.IsNullOrEmpty(name))
                        {
                            var indexOfDot = name.LastIndexOf(".");
                            if (indexOfDot > 0 && indexOfDot < name.Length - 1)
                            {
                                name = name.Remove(0, indexOfDot);
                            }
                        }
                    }

                    bool previousWasnotLetter = true;
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (char.IsLetter(name[i]))
                        {
                            rsb.Append(previousWasnotLetter ? char.ToUpper(name[i]) : name[i]);
                            previousWasnotLetter = false;
                            continue;
                        }
                        if (char.IsDigit(name[i]) && i > 0)
                        {
                            rsb.Append(name[i]);
                        }
                        previousWasnotLetter = true;
                    }

                    if (rsb.Length > 1)
                    {
                        var result = rsb.ToString().Trim(' ', '.', ':', '-');
                        if (!string.IsNullOrEmpty(result))
                        {
                            return result[0].ToString().ToUpper() + result.Remove(0, 1);
                        }
                        return result;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
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