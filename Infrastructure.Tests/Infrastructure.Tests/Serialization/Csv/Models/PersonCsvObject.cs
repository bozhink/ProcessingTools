namespace ProcessingTools.Infrastructure.Serialization.Csv.Tests.Models
{
    [CsvObject]
    internal class PersonCsvObject
    {
        [CsvColumn("first name")]
        public string FirstName { get; set; }

        [CsvColumn("last name")]
        public string LastName { get; set; }
    }
}