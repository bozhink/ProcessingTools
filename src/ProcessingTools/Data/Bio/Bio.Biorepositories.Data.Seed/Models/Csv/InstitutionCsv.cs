namespace ProcessingTools.Bio.Biorepositories.Data.Seed.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Serialization.Csv;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Biorepositories;

    [FileName("grbio_institutions.csv")]
    [CsvObject]
    public class InstitutionCsv : IInstitution
    {
        private string institutionCode;

        [CsvColumn("Additional Institution Names")]
        public string AdditionalInstitutionNames { get; set; }

        [CsvColumn("CITES permit number")]
        public string CitesPermitNumber { get; set; }

        [CsvColumn("Cool URI")]
        public string CoolUri { get; set; }

        [CsvColumn("Date herbarium was founded")]
        public string DateHerbariumWasFounded { get; set; }

        [CsvColumn("Description of Institution")]
        public string DescriptionOfInstitution { get; set; }

        [CsvColumn("Geographic coverage of herbarium")]
        public string GeographicCoverageOfHerbarium { get; set; }

        [CsvColumn("Incorporated Herbaria")]
        public string IncorporatedHerbaria { get; set; }

        [CsvColumn("Index Herbariorum Record")]
        public IndexHerbariorumRecordType IndexHerbariorumRecord { get; set; }

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

        [CsvColumn("Institution Name")]
        public string InstitutionName { get; set; }

        [CsvColumn("Institution Type")]
        public string InstitutionType { get; set; }

        [CsvColumn("Institutional Discipline")]
        public string InstitutionalDiscipline { get; set; }

        [CsvColumn("Institutional Governance")]
        public string InstitutionalGovernance { get; set; }

        [CsvColumn("Institutional LSID")]
        public string InstitutionalLsid { get; set; }

        [CsvColumn("Number of specimens in herbarium")]
        public string NumberOfSpecimensInHerbarium { get; set; }

        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        [CsvColumn("Serials published by Institution")]
        public string SerialsPublishedByInstitution { get; set; }

        [CsvColumn("Status of Institution")]
        public string StatusOfInstitution { get; set; }

        [CsvColumn("Taxonomic coverage of herbarium")]
        public string TaxonomicCoverageOfHerbarium { get; set; }

        [CsvColumn("URL for institutional specimen catalog")]
        public string UrlForInstitutionalSpecimenCatalog { get; set; }

        [CsvColumn("URL for institutional webservices")]
        public string UrlForInstitutionalWebservices { get; set; }

        [CsvColumn("URL for main institutional website")]
        public string UrlForMainInstitutionalWebsite { get; set; }

        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}
