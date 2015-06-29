namespace GradleBindings.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string androidSdk = @"C:\Users\Egorbo\AppData\Local\Android\sdk";
            const string testGradleFile = @"E:\demo\build.gradle";

            var result = new Gradle(androidSdk)
                .ExtractDependencies(testGradleFile);
        }
    }
}
