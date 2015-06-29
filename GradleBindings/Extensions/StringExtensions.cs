namespace GradleBindings.Extensions
{
    public static class StringExtensions
    {
        public static string FixPathForGradle(this string path)
        {
            return path.Replace("\\", "/");
        }
    }
}
