using System;
using System.IO;
using System.Linq;

namespace GradleBindings
{
    public static class SystemPaths
    {
        public static string GetAndroidSdkPath()
        {
            //TODO: ask IDE? it should know :)
            string repoFolder;
            var androidSdk = new[] { "ANDROID_HOME", "ANDROID_SDK", "ANDROID_SDK_ROOT" }
                .Select(i => Environment.GetEnvironmentVariable(i))
                .Concat(new[] { Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @"AppData\Local\Android\sdk") })
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .OrderByDescending(i => Gradle.HasLocalRepositories(i, out repoFolder))
                .FirstOrDefault();

            return androidSdk;
        }

        public static string GetJavaHome()
        {
            var javaFolders = Directory.GetFiles(Path.Combine(GetProgramFiles(), "Java"))
                .Concat(Directory.GetFiles(Path.Combine(GetProgramFilesX86(), "Java")))
                .Where(f => f.StartsWith("jdk"))
                .OrderByDescending(i => i); //TODO: order via Regex (jdk1.8_xx)
            return javaFolders.FirstOrDefault();
        }

        public static string GetProgramFiles() => 
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

        public static string GetProgramFilesX86() =>
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Replace("(x86)", "").TrimEnd();
    }
}
