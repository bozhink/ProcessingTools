namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv
{
    using ProcessingTools.Attributes;
    using ProcessingTools.Common.Serialization.Csv;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    [FileName("grbio_collections_labels.csv")]
    [CsvObject]
    public class InstitutionLabelCsv : IInstitutionLabel
    {
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        [CsvColumn("Country")]
        public string Country { get; set; }

        [CsvColumn("Name of Institution")]
        public string NameOfInstitution { get; set; }

        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }
    }
}
