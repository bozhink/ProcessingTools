namespace ProcessingTools.Serialization.Csv
{
    /// <summary>
    /// Provides configuration parameters for serialization of CSV.
    /// </summary>
    public class CsvObjectConfiguration
    {
        public CsvObjectConfiguration(
            char fieldTerminator = ',',
            char rowTerminator = '\n',
            int firstRow = 1,
            char singleCharEscapeSymbol = '\\',
            char terminatorEscapeLeftWrapSymbol = '"',
            char terminatorEscapeRightWrapSymbol = '"')
        {
            this.FieldTerminator = fieldTerminator;
            this.RowTerminator = rowTerminator;
            this.FirstRow = firstRow;
            this.SingleCharEscapeSymbol = singleCharEscapeSymbol;
            this.TerminatorEscapeLeftWrapSymbol = terminatorEscapeLeftWrapSymbol;
            this.TerminatorEscapeRightWrapSymbol = terminatorEscapeRightWrapSymbol;
        }

        public char FieldTerminator { get; set; }

        public char RowTerminator { get; set; }

        public int FirstRow { get; set; }

        public char SingleCharEscapeSymbol { get; set; }

        public char TerminatorEscapeLeftWrapSymbol { get; set; }

        public char TerminatorEscapeRightWrapSymbol { get; set; }
    }
}