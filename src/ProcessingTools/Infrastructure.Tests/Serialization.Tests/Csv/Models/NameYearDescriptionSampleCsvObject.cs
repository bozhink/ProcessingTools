namespace ProcessingTools.Serialization.Tests.Csv.Models
{
    using ProcessingTools.Common.Serialization.Csv;

    [CsvObject]
    internal class NameYearDescriptionSampleCsvObject
    {
        [CsvColumn]
        public string Name { get; set; }

        [CsvColumn]
        public int Year { get; set; }

        [CsvColumn]
        public string Description { get; set; }
    }
}
