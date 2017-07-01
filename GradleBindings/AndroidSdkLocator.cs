using System;
using System.IO;
using System.Linq;

namespace GradleBindings
{
    public static class AndroidSdkLocator
    {
        public static string Locate()
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
    }
}
