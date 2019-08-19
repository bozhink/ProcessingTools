// <copyright file="CollectionPerCsv.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Collection per CSV model.
    /// </summary>
    [FileName("grbio_collections_pers.csv")]
    [CsvObject]
    public class CollectionPerCsv : ICollectionPer
    {
        /// <summary>
        /// Gets or sets the "Access Eligibility and Rules" column value.
        /// </summary>
        [CsvColumn("Access Eligibility and Rules")]
        public string AccessEligibilityAndRules { get; set; }

        /// <summary>
        /// Gets or sets the "Collection Code" column value.
        /// </summary>
        [CsvColumn("Collection Code")]
        public string CollectionCode { get; set; }

        /// <summary>
        /// Gets or sets the "Collection Content Type" column value.
        /// </summary>
        [CsvColumn("Collection Content Type")]
        public string CollectionContentType { get; set; }

        /// <summary>
        /// Gets or sets the "Collection Description" column value.
        /// </summary>
        [CsvColumn("Collection Description")]
        public string CollectionDescription { get; set; }

        /// <summary>
        /// Gets or sets the "Collection Name" column value.
        /// </summary>
        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the "Cool URI" column value.
        /// </summary>
        [CsvColumn("Cool URI")]
        public string CoolUri { get; set; }

        /// <summary>
        /// Gets or sets the "Institution Name" column value.
        /// </summary>
        [CsvColumn("Institution Name")]
        public string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets the "LSID" column value.
        /// </summary>
        [CsvColumn("LSID")]
        public string Lsid { get; set; }

        /// <summary>
        /// Gets or sets "Preservation Type" column value.
        /// </summary>
        [CsvColumn("Preservation Type")]
        public string PreservationType { get; set; }

        /// <summary>
        /// Gets or sets "Primary Contact" column value.
        /// </summary>
        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets "Status of Collection" column value.
        /// </summary>
        [CsvColumn("Status of Collection")]
        public string StatusOfCollection { get; set; }

        /// <summary>
        /// Gets or sets "URL for collection" column value.
        /// </summary>
        [CsvColumn("URL for collection")]
        public string UrlForCollection { get; set; }

        /// <summary>
        /// Gets or sets "URL for collection's specimen catalog/database" column value.
        /// </summary>
        [CsvColumn("URL for collection's specimen catalog/database")]
        public string UrlForCollectionSpecimenCatalog { get; set; }

        /// <summary>
        /// Gets or sets "URL for collection's webservices" column value.
        /// </summary>
        [CsvColumn("URL for collection's webservices")]
        public string UrlForCollectionWebservices { get; set; }

        /// <summary>
        /// Gets or sets "URL" column value.
        /// </summary>
        [CsvColumn("URL")]
        public string Url { get; set; }
    }
}
