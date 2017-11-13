// <copyright file="EnBgTransliteration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Language
{
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Processors.Contracts.Language;

    /// <summary>
    /// English-to-Bulgarian transliteration.
    /// </summary>
    public class EnBgTransliteration : IEnBgTransliteration
    {
        private readonly IDictionary<string, string> dictionary = new Dictionary<string, string>
        {
            { "A", "А" },
            { "B", "Б" },
            { "V", "В" },
            { "G", "Г" },
            { "D", "Д" },
            { "E", "Е" },
            { "Zh", "Ж" },
            { "Z", "З" },
            { "I", "И" },
            { "Y", "Й" },
            { "K", "К" },
            { "L", "Л" },
            { "M", "М" },
            { "N", "Н" },
            { "O", "О" },
            { "P", "П" },
            { "R", "Р" },
            { "S", "С" },
            { "T", "Т" },
            { "U", "У" },
            { "F", "Ф" },
            { "H", "Х" },
            { "Ts", "Ц" },
            { "Ch", "Ч" },
            { "Sh", "Ш" },
            { "Sht", "Щ" },
            { "Yu", "Ю" },
            { "Ya", "Я" },
            { "a", "а" },
            { "b", "б" },
            { "v", "в" },
            { "g", "г" },
            { "d", "д" },
            { "e", "е" },
            { "zh", "ж" },
            { "z", "з" },
            { "i", "и" },
            { "y", "й" },
            { "k", "к" },
            { "l", "л" },
            { "m", "м" },
            { "n", "н" },
            { "o", "о" },
            { "p", "п" },
            { "r", "р" },
            { "s", "с" },
            { "t", "т" },
            { "u", "у" },
            { "f", "ф" },
            { "h", "х" },
            { "ts", "ц" },
            { "ch", "ч" },
            { "sh", "ш" },
            { "sht", "щ" },
            { "yu", "ю" },
            { "ya", "я" },
        };

        /// <inheritdoc/>
        public string ProcessText(string text)
        {
            string result = text;
            if (string.IsNullOrWhiteSpace(result))
            {
                return result;
            }

            foreach (var item in this.dictionary.OrderByDescending(i => i.Key.Length))
            {
                result = result.Replace(item.Key, item.Value);
            }

            return result;
        }
    }
}
