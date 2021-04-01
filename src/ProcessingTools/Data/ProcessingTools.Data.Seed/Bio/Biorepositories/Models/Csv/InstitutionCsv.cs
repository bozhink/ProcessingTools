// <copyright file="InstitutionCsv.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Institution CSV model.
    /// </summary>
    [FileName("grbio_institutions.csv")]
    [CsvObject]
    public class InstitutionCsv : IInstitution
    {
        private string institutionCode;

        /// <summary>
        /// Gets or sets "Additional Institution Names" column value.
        /// </summary>
        [CsvColumn("Additional Institution Names")]
        public string AdditionalInstitutionNames { get; set; }

        /// <summary>
        /// Gets or sets "CITES permit number" column value.
        /// </summary>
        [CsvColumn("CITES permit number")]
        public string CitesPermitNumber { get; set; }

        /// <summary>
        /// Gets or sets "Cool URI" column value.
        /// </summary>
        [CsvColumn("Cool URI")]
        public string CoolUri { get; set; }

        /// <summary>
        /// Gets or sets "Date herbarium was founded" column value.
        /// </summary>
        [CsvColumn("Date herbarium was founded")]
        public string DateHerbariumWasFounded { get; set; }

        /// <summary>
        /// Gets or sets "Description of Institution" column value.
        /// </summary>
        [CsvColumn("Description of Institution")]
        public string DescriptionOfInstitution { get; set; }

        /// <summary>
        /// Gets or sets "Geographic coverage of herbarium" column value.
        /// </summary>
        [CsvColumn("Geographic coverage of herbarium")]
        public string GeographicCoverageOfHerbarium { get; set; }

        /// <summary>
        /// Gets or sets "Incorporated Herbaria" column value.
        /// </summary>
        [CsvColumn("Incorporated Herbaria")]
        public string IncorporatedHerbaria { get; set; }

        /// <summary>
        /// Gets or sets "Index Herbariorum Record" column value.
        /// </summary>
        [CsvColumn("Index Herbariorum Record")]
        public IndexHerbariorumRecordType IndexHerbariorumRecord { get; set; }

        /// <summary>
        /// Gets or sets "Institution Code" column value.
        /// </summary>
        [CsvColumn("Institution Code")]
        public string InstitutionCode
        {
            get
            {
                return this.institutionCode;
            }

            set
            {
                this.institutionCode = value.Replace("<IH>", string.Empty);
            }
        }

        /// <summary>
        /// Gets or sets "Institution Name" column value.
        /// </summary>
        [CsvColumn("Institution Name")]
        public string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets "Institution Type" column value.
        /// </summary>
        [CsvColumn("Institution Type")]
        public string InstitutionType { get; set; }

        /// <summary>
        /// Gets or sets "Institutional Discipline" column value.
        /// </summary>
        [CsvColumn("Institutional Discipline")]
        public string InstitutionalDiscipline { get; set; }

        /// <summary>
        /// Gets or sets "Institutional Governance" column value.
        /// </summary>
        [CsvColumn("Institutional Governance")]
        public string InstitutionalGovernance { get; set; }

        /// <summary>
        /// Gets or sets "Institutional LSID" column value.
        /// </summary>
        [CsvColumn("Institutional LSID")]
        public string InstitutionalLsid { get; set; }

        /// <summary>
        /// Gets or sets "Number of specimens in herbarium" column value.
        /// </summary>
        [CsvColumn("Number of specimens in herbarium")]
        public string NumberOfSpecimensInHerbarium { get; set; }

        /// <summary>
        /// Gets or sets "Primary Contact" column value.
        /// </summary>
        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets "Serials published by Institution" column value.
        /// </summary>
        [CsvColumn("Serials published by Institution")]
        public string SerialsPublishedByInstitution { get; set; }

        /// <summary>
        /// Gets or sets "Status of Institution" column value.
        /// </summary>
        [CsvColumn("Status of Institution")]
        public string StatusOfInstitution { get; set; }

        /// <summary>
        /// Gets or sets "Taxonomic coverage of herbarium" column value.
        /// </summary>
        [CsvColumn("Taxonomic coverage of herbarium")]
        public string TaxonomicCoverageOfHerbarium { get; set; }

        /// <summary>
        /// Gets or sets "URL for institutional specimen catalog" column value.
        /// </summary>
        [CsvColumn("URL for institutional specimen catalog")]
        public string UrlForInstitutionalSpecimenCatalog { get; set; }

        /// <summary>
        /// Gets or sets "URL for institutional webservices" column value.
        /// </summary>
        [CsvColumn("URL for institutional webservices")]
        public string UrlForInstitutionalWebservices { get; set; }

        /// <summary>
        /// Gets or sets "URL for main institutional website" column value.
        /// </summary>
        [CsvColumn("URL for main institutional website")]
        public string UrlForMainInstitutionalWebsite { get; set; }

        /// <summary>
        /// Gets or sets "URL" column value.
        /// </summary>
        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}
