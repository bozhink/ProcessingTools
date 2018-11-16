namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Code.Serialization.Csv;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    [FileName("grbio_staff_labels.csv")]
    [CsvObject]
    public class StaffLabelCsv : IStaffLabel
    {
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        [CsvColumn("Country")]
        public string Country { get; set; }

        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        [CsvColumn("Primary Institution")]
        public string PrimaryInstitution { get; set; }

        [CsvColumn("Staff Member Full Name")]
        public string StaffMemberFullName { get; set; }

        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }
    }
}
