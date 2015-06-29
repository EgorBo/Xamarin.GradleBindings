using System;
using System.Linq;

namespace GradleBindings.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var androidSdk = Environment.GetEnvironmentVariable("ANDROID_HOME");

            var result = Gradle.ExtractDependencies("com.makeramen:roundedimageview:2.1.0", @"C:\Users\Egorbo\AppData\Local\Android\sdk")
                .OrderBy(f => f.IsDependency)
                .ToList();

            foreach (var file in result)
            {
                System.Console.WriteLine("{0} [main: {1}]", file.File, !file.IsDependency);
            }
            System.Console.ReadKey();
        }
    }
}
