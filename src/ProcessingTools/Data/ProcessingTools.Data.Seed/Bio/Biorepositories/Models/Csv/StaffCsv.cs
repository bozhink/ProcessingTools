// <copyright file="StaffCsv.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Staff CSV model.
    /// </summary>
    [FileName("grbio_staff.csv")]
    [CsvObject]
    public class StaffCsv : IStaff
    {
        /// <summary>
        /// Gets or sets "Additional Affiliations" column value.
        /// </summary>
        [CsvColumn("Additional Affiliations")]
        public string AdditionalAffiliations { get; set; }

        /// <summary>
        /// Gets or sets "Area of Responsibility" column value.
        /// </summary>
        [CsvColumn("Area of Responsibility")]
        public string AreaOfResponsibility { get; set; }

        /// <summary>
        /// Gets or sets "Birth Year" column value.
        /// </summary>
        [CsvColumn("Birth Year")]
        public string BirthYear { get; set; }

        /// <summary>
        /// Gets or sets "City/Town" column value.
        /// </summary>
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        /// <summary>
        /// Gets or sets "Country" column value.
        /// </summary>
        [CsvColumn("Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets "E-mail" column value.
        /// </summary>
        [CsvColumn("E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets "Fax Number" column value.
        /// </summary>
        [CsvColumn("Fax Number")]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets "First Name" column value.
        /// </summary>
        [CsvColumn("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets "Job Title" column value.
        /// </summary>
        [CsvColumn("Job Title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets "Last Name" column value.
        /// </summary>
        [CsvColumn("Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets "Phone Number" column value.
        /// </summary>
        [CsvColumn("Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets "Postal/Zip Code" column value.
        /// </summary>
        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        /// <summary>
        /// Gets or sets "Primary Collection" column value.
        /// </summary>
        [CsvColumn("Primary Collection")]
        public string PrimaryCollection { get; set; }

        /// <summary>
        /// Gets or sets "Primary Institution" column value.
        /// </summary>
        [CsvColumn("Primary Institution")]
        public string PrimaryInstitution { get; set; }

        /// <summary>
        /// Gets or sets "Research Discipline" column value.
        /// </summary>
        [CsvColumn("Research Discipline")]
        public string ResearchDiscipline { get; set; }

        /// <summary>
        /// Gets or sets "Research Specialty" column value.
        /// </summary>
        [CsvColumn("Research Specialty")]
        public string ResearchSpecialty { get; set; }

        /// <summary>
        /// Gets or sets "Staff Member Full Name" column value.
        /// </summary>
        [CsvColumn("Staff Member Full Name")]
        public string StaffMemberFullName { get; set; }

        /// <summary>
        /// Gets or sets "State/Province" column value.
        /// </summary>
        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }

        /// <summary>
        /// Gets or sets "URL" column value.
        /// </summary>
        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}
