using System.Text;

namespace TreeBasedSearch.Utilities
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all strings in a given array from a string.
        /// </summary>
        /// <param name="value">The original string.</param>
        /// <param name="array">An array of strings to be removed.</param>
        /// <returns>A copy of the original string with the given strings removed.</returns>
        public static string Remove(this string value, params string[] array)
        {
            StringBuilder builder = new StringBuilder(value);
            foreach (string item in array) builder.Replace(item, string.Empty);
            return builder.ToString();
        }
    }
}