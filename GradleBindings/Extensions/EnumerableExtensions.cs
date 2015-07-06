using System.Collections;

namespace GradleBindings.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return true;

            foreach (var item in enumerable)
            {
                return false;
            }
            return true;
        }
    }
}
