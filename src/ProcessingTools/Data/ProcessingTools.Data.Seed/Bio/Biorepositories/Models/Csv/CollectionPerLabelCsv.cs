namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Code.Serialization.Csv;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    [FileName("grbio_collections_pers_labels.csv")]
    [CsvObject]
    public class CollectionPerLabelCsv : ICollectionPerLabel
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
