// <copyright file="RomanTransliteration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Language
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ProcessingTools.Contracts.Processors.Language;

    /// <summary>
    /// Roman-to-Arabic transliteration.
    /// </summary>
    public class RomanTransliteration : IRomanTransliteration
    {
        private readonly IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            { "I", "1" },
            { "II", "2" },
            { "III", "3" },
            { "IV", "4" },
            { "V", "5" },
            { "VI", "6" },
            { "VII", "7" },
            { "VIII", "8" },
            { "IX", "9" },
            { "X", "10" },
            { "XI", "11" },
            { "XII", "12" }
        };

        /// <inheritdoc/>
        public string ProcessText(string text)
        {
            string result = text;
            if (string.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            result = result.Replace('Х', 'X').Replace('V', 'V').Replace('І', 'I');

            foreach (var item in this.dictionary.OrderByDescending(i => i.Key.Length))
            {
                result = Regex.Replace(result, "(?i)" + item.Key, item.Value);
            }

            return result;
        }
    }
}
