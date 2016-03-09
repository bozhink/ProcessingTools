namespace ProcessingTools.Bio.Biorepositories.Data.Models.Seed.Csv
{
    using ProcessingTools.Infrastructure.Serialization.Csv;

    [CsvObject]
    public class CollectionPerLabel
    {
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        [CsvColumn("Country")]
        public string Country { get; set; }

        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }
    }
}