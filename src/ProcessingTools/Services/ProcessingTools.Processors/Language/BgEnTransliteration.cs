// <copyright file="BgEnTransliteration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Language
{
    using System.Collections.Generic;
    using System.Text;
    using ProcessingTools.Processors.Contracts.Language;

    /// <summary>
    /// Bulgarian-to-English transliteration.
    /// </summary>
    public class BgEnTransliteration : IBgEnTransliteration
    {
        private readonly IDictionary<char, string> dictionary = new Dictionary<char, string>
        {
            { 'А', "A" },
            { 'Б', "B" },
            { 'В', "V" },
            { 'Г', "G" },
            { 'Д', "D" },
            { 'Е', "E" },
            { 'Ж', "Zh" },
            { 'З', "Z" },
            { 'И', "I" },
            { 'Й', "Y" },
            { 'К', "K" },
            { 'Л', "L" },
            { 'М', "M" },
            { 'Н', "N" },
            { 'О', "O" },
            { 'П', "P" },
            { 'Р', "R" },
            { 'С', "S" },
            { 'Т', "T" },
            { 'У', "U" },
            { 'Ф', "F" },
            { 'Х', "H" },
            { 'Ц', "Ts" },
            { 'Ч', "Ch" },
            { 'Ш', "Sh" },
            { 'Щ', "Sht" },
            { 'Ъ', "A" },
            { 'Ь', "Y" },
            { 'Ю', "Yu" },
            { 'Я', "Ya" },
            { 'а', "a" },
            { 'б', "b" },
            { 'в', "v" },
            { 'г', "g" },
            { 'д', "d" },
            { 'е', "e" },
            { 'ж', "zh" },
            { 'з', "z" },
            { 'и', "i" },
            { 'й', "y" },
            { 'к', "k" },
            { 'л', "l" },
            { 'м', "m" },
            { 'н', "n" },
            { 'о', "o" },
            { 'п', "p" },
            { 'р', "r" },
            { 'с', "s" },
            { 'т', "t" },
            { 'у', "u" },
            { 'ф', "f" },
            { 'х', "h" },
            { 'ц', "ts" },
            { 'ч', "ch" },
            { 'ш', "sh" },
            { 'щ', "sht" },
            { 'ъ', "a" },
            { 'ь', "y" },
            { 'ю', "yu" },
            { 'я', "ya" }
        };

        /// <inheritdoc/>
        public string ProcessText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var stringBuilder = new StringBuilder();
            var characters = text.ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                char character = characters[i];

                if (this.dictionary.ContainsKey(character))
                {
                    stringBuilder.Append(this.dictionary[character]);
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
