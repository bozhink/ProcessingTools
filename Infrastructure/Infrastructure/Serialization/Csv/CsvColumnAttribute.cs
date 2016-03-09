namespace ProcessingTools.Infrastructure.Serialization.Csv
{
    using System;

    public class CsvColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}