using System.Globalization;

namespace GradleBindings.Extensions
{
    public static class StringExtensions
    {
        public static string FixPathForGradle(this string path)
        {
            return path.Replace("\\", "/");
        }

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str[0].ToString().ToUpper() + str.Remove(0, 1);
        }
    }
}
