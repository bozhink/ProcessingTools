namespace ProcessingTools.Extensions
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class RegexExtensions
    {
        public static IEnumerable<string> ToIEnumerable(this Match match)
        {
            for (var m = match; m.Success; m = m.NextMatch())
            {
                yield return m.Value;
            }
        }
    }
}
