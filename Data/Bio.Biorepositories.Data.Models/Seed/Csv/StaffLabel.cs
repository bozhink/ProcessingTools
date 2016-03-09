namespace ProcessingTools.Bio.Biorepositories.Data.Models.Seed.Csv
{
    using ProcessingTools.Infrastructure.Serialization.Csv;

    [CsvObject]
    public class StaffLabel
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