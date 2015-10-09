namespace ProcessingTools
{
    using System.Text.RegularExpressions;

    public static class TextExtensions
    {
        public static string RegexReplace(this string target, string regexPattern, string regexReplacement)
        {
            return Regex.Replace(target, regexPattern, regexReplacement);
        }

        public static string RegexReplace(this string target, Regex regex, string regexReplacement)
        {
            return regex.Replace(target, regexReplacement);
        }
    }
}
