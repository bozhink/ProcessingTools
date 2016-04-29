namespace ProcessingTools.Bio.Biorepositories.Data.Models.Seed.Csv
{
    using ProcessingTools.Attributes;
    using ProcessingTools.Serialization.Csv;

    [FileName("grbio_staff.csv")]
    [CsvObject]
    public class Staff
    {
        [CsvColumn("Additional Affiliations")]
        public string AdditionalAffiliations { get; set; }

        [CsvColumn("Area of Responsibility")]
        public string AreaOfResponsibility { get; set; }

        [CsvColumn("Birth Year")]
        public string BirthYear { get; set; }

        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        [CsvColumn("Country")]
        public string Country { get; set; }

        [CsvColumn("E-mail")]
        public string Email { get; set; }

        [CsvColumn("Fax Number")]
        public string FaxNumber { get; set; }

        [CsvColumn("First Name")]
        public string FirstName { get; set; }

        [CsvColumn("Job Title")]
        public string JobTitle { get; set; }

        [CsvColumn("Last Name")]
        public string LastName { get; set; }

        [CsvColumn("Phone Number")]
        public string PhoneNumber { get; set; }

        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        [CsvColumn("Primary Collection")]
        public string PrimaryCollection { get; set; }

        [CsvColumn("Primary Institution")]
        public string PrimaryInstitution { get; set; }

        [CsvColumn("Research Discipline")]
        public string ResearchDiscipline { get; set; }

        [CsvColumn("Research Specialty")]
        public string ResearchSpecialty { get; set; }

        [CsvColumn("Staff Member Full Name")]
        public string StaffMemberFullName { get; set; }

        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }

        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}