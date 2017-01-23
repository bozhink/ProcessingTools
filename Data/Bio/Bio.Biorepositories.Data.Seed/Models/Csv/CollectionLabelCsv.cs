namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv
{
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models;
    using ProcessingTools.Serialization.Csv;

    [FileName("grbio_collections_labels.csv")]
    [CsvObject]
    public class CollectionLabelCsv : ICollectionLabel
    {
        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }

        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        [CsvColumn("Country")]
        public string Country { get; set; }
    }
}
