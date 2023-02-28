using System.Text.RegularExpressions;

namespace QuizPop.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Uses regex to convert a string to snake case
        /// </summary>
        /// <param name="str">The string var itself</param>
        /// <returns>A new string converted to snake case</returns>
        public static string ToSnakeCase(this string str) => Regex.Replace(str, "([a-z])([A-Z])", "$1_$2").ToLower();
    }
}
