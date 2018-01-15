namespace ProcessingTools.Common.Serialization.Csv
{
    using System;

    public class CsvColumnAttribute : Attribute
    {
        public CsvColumnAttribute()
        {
        }

        public CsvColumnAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}