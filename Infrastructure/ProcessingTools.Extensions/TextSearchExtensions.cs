namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class TextSearchExtensions
    {
        public static IEnumerable<string> GetMatches(this string text, Regex search)
        {
            var result = new HashSet<string>();

            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            return result;
        }
    }
}