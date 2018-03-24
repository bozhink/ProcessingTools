// <copyright file="CsvTableReader.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// CSV table reader.
    /// </summary>
    public class CsvTableReader
    {
        private readonly Queue<string[]> rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        public CsvTableReader()
            : this(new CsvObjectConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTableReader"/> class.
        /// </summary>
        /// <param name="configuration">The CSV object configuration.</param>
        public CsvTableReader(CsvObjectConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.rows = new Queue<string[]>();
        }

        /// <summary>
        /// Gets the CSV object configuration.
        /// </summary>
        public CsvObjectConfiguration Configuration { get; private set; }

        /// <summary>
        /// Reads CSV as string to table of strings.
        /// </summary>
        /// <param name="text">CSV string to be read.</param>
        /// <returns>Processed table of strings.</returns>
        public string[][] ReadToTable(string text)
        {
            char[] textChars = text.Trim(' ', '\r', '\n').ToCharArray();

            Queue<string> fields = new Queue<string>();
            StringBuilder stringBuilder = new StringBuilder();

            bool escapeState = false;
            for (int i = 0, len = textChars.Length; i < len; ++i)
            {
                char ch = textChars[i];
                if (ch == this.Configuration.SingleCharEscapeSymbol)
                {
                    // Do not include the escape char in output
                    //// stringBuilder.Append(ch);
                    if (i >= len - 1)
                    {
                        throw new FormatException("Invalid escape of last character of the text.");
                    }

                    stringBuilder.Append(textChars[++i]);
                }
                else if (ch == this.Configuration.SeparatorEscapeLeftWrapSymbol && ch == this.Configuration.SeparatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    //// stringBuilder.Append(ch);
                    escapeState = !escapeState;
                }
                else if (ch == this.Configuration.SeparatorEscapeLeftWrapSymbol)
                {
                    // Do not include the escape char in output
                    //// stringBuilder.Append(ch);
                    if (!escapeState)
                    {
                        escapeState = true;
                    }
                }
                else if (ch == this.Configuration.SeparatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    //// stringBuilder.Append(ch);
                    if (escapeState)
                    {
                        escapeState = false;
                    }
                }
                else if (ch == this.Configuration.FieldSeparator)
                {
                    if (escapeState)
                    {
                        stringBuilder.Append(ch);
                    }
                    else
                    {
                        fields.Enqueue(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                }
                else if (ch == this.Configuration.RowSeparator)
                {
                    if (escapeState)
                    {
                        stringBuilder.Append(ch);
                    }
                    else
                    {
                        // The row terminator is actually a field terminator
                        fields.Enqueue(stringBuilder.ToString());
                        stringBuilder.Clear();

                        this.rows.Enqueue(fields.ToArray());
                        fields = new Queue<string>();
                    }
                }
                else
                {
                    stringBuilder.Append(ch);
                }
            }

            fields.Enqueue(stringBuilder.ToString());
            stringBuilder.Clear();

            this.rows.Enqueue(fields.ToArray());

            return this.rows.ToArray();
        }
    }
}
