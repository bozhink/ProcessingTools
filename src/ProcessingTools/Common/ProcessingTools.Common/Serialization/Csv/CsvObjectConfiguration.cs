// <copyright file="CsvObjectConfiguration.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization.Csv
{
    /// <summary>
    /// Provides configuration parameters for serialization of CSV.
    /// </summary>
    public class CsvObjectConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvObjectConfiguration"/> class.
        /// </summary>
        /// <param name="fieldSeparator">Field separator sign.</param>
        /// <param name="rowSeparator">Row separator sign.</param>
        /// <param name="firstRow">Number of the first row.</param>
        /// <param name="singleCharEscapeSymbol">Escape sign for single symbol.</param>
        /// <param name="separatorEscapeLeftWrapSymbol">Left wrap escape sign for separators.</param>
        /// <param name="separatorEscapeRightWrapSymbol">Right wrap escape sign for separators.</param>
        public CsvObjectConfiguration(
            char fieldSeparator = ',',
            char rowSeparator = '\n',
            int firstRow = 1,
            char singleCharEscapeSymbol = '\\',
            char separatorEscapeLeftWrapSymbol = '"',
            char separatorEscapeRightWrapSymbol = '"')
        {
            this.FieldSeparator = fieldSeparator;
            this.RowSeparator = rowSeparator;
            this.FirstRow = firstRow;
            this.SingleCharEscapeSymbol = singleCharEscapeSymbol;
            this.SeparatorEscapeLeftWrapSymbol = separatorEscapeLeftWrapSymbol;
            this.SeparatorEscapeRightWrapSymbol = separatorEscapeRightWrapSymbol;
        }

        /// <summary>
        /// Gets or sets the field separator sign.
        /// </summary>
        public char FieldSeparator { get; set; }

        /// <summary>
        /// Gets or sets the row separator sign.
        /// </summary>
        public char RowSeparator { get; set; }

        /// <summary>
        /// Gets or sets the number of the first row.
        /// </summary>
        public int FirstRow { get; set; }

        /// <summary>
        /// Gets or sets the escape sign for single symbol.
        /// </summary>
        public char SingleCharEscapeSymbol { get; set; }

        /// <summary>
        /// Gets or sets the left wrap escape sign for separators.
        /// </summary>
        public char SeparatorEscapeLeftWrapSymbol { get; set; }

        /// <summary>
        /// Gets or sets the right wrap escape sign for separators.
        /// </summary>
        public char SeparatorEscapeRightWrapSymbol { get; set; }
    }
}
