namespace ProcessingTools.Serialization.Tests.Csv.Models
{
    using ProcessingTools.Serialization.Csv;

    [CsvObject]
    internal class PersonCsvObject
    {
        [CsvColumn("first name")]
        public string FirstName { get; set; }

        [CsvColumn("last name")]
        public string LastName { get; set; }
    }
}