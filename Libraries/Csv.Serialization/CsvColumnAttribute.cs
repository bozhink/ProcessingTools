namespace ProcessingTools.Csv.Serialization
{
    using System;

    public class CsvColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}