namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv
{
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Biorepositories.Data.Common.Contracts.Models;
    using ProcessingTools.Serialization.Csv;

    [FileName("grbio_staff_labels.csv")]
    [CsvObject]
    public class StaffLabel : IStaffLabel
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
