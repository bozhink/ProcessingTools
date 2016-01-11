namespace ProcessingTools.Csv.Serialization
{
    using System;

    public class CsvObjectAttribute : Attribute
    {
        public char FieldTerminator { get; set; }

        public char RowTerminator { get; set; }

        public int FirstRow { get; set; }

        public char SingleCharEscapeSymbol { get; set; }

        public char TerminatorEscapeLeftWrapSymbol { get; set; }

        public char TerminatorEscapeRightWrapSymbol { get; set; }

        public CsvObjectConfiguration Configuration { get; set; }
    }
}
