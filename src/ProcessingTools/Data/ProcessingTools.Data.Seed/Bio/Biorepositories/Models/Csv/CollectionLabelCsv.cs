// <copyright file="CollectionLabelCsv.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Collection label CSV model.
    /// </summary>
    [FileName("grbio_collections_labels.csv")]
    [CsvObject]
    public class CollectionLabelCsv : ICollectionLabel
    {
        /// <summary>
        /// Gets or sets the "Collection Name" column value.
        /// </summary>
        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the "Primary Contact" column value.
        /// </summary>
        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets the "City/Town" column value.
        /// </summary>
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        /// <summary>
        /// Gets or sets the "State/Province" column value.
        /// </summary>
        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }

        /// <summary>
        /// Gets or sets the "Postal/Zip Code" column value.
        /// </summary>
        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        /// <summary>
        /// Gets or sets the "Country" column value.
        /// </summary>
        [CsvColumn("Country")]
        public string Country { get; set; }
    }
}
