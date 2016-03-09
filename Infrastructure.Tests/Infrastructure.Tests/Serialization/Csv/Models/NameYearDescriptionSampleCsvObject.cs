namespace ProcessingTools.Infrastructure.Serialization.Csv.Tests.Models
{
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