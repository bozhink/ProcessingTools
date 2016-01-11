namespace ProcessingTools.Csv.Serialization.Tests.Models
{
    [CsvObject]
    internal class PersonCsvObject
    {
        [CsvColumn(Name = "first name")]
        public string FirstName { get; set; }

        [CsvColumn(Name = "last name")]
        public string LastName { get; set; }
    }
}