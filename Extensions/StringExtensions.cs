using System.Text.RegularExpressions;

namespace QuizPop.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string str) => Regex.Replace(str, "([a-z])([A-Z])", "$1_$2").ToLower();
    }
}
