namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Serialization.Csv;

    [FileName("grbio_collections_pers.csv")]
    [CsvObject]
    public class CollectionPerCsv : ICollectionPer
    {
        [CsvColumn("Access Eligibility and Rules")]
        public string AccessEligibilityAndRules { get; set; }

        [CsvColumn("Collection Code")]
        public string CollectionCode { get; set; }

        [CsvColumn("Collection Content Type")]
        public string CollectionContentType { get; set; }

        [CsvColumn("Collection Description")]
        public string CollectionDescription { get; set; }

        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        [CsvColumn("Cool URI")]
        public string CoolUri { get; set; }

        [CsvColumn("Institution Name")]
        public string InstitutionName { get; set; }

        [CsvColumn("LSID")]
        public string Lsid { get; set; }

        [CsvColumn("Preservation Type")]
        public string PreservationType { get; set; }

        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        [CsvColumn("Status of Collection")]
        public string StatusOfCollection { get; set; }

        [CsvColumn("URL for collection")]
        public string UrlForCollection { get; set; }

        [CsvColumn("URL for collection's specimen catalog/database")]
        public string UrlForCollectionSpecimenCatalog { get; set; }

        [CsvColumn("URL for collection's webservices")]
        public string UrlForCollectionWebservices { get; set; }

        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}
