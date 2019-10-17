﻿// <copyright file="CollectionPerLabelCsv.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Bio.Biorepositories.Models.Csv
{
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Serialization.Csv;

    /// <summary>
    /// Collection per label CSV Model.
    /// </summary>
    [FileName("grbio_collections_pers_labels.csv")]
    [CsvObject]
    public class CollectionPerLabelCsv : ICollectionPerLabel
    {
        /// <summary>
        /// Gets or sets "City/Town" column value.
        /// </summary>
        [CsvColumn("City/Town")]
        public string CityTown { get; set; }

        /// <summary>
        /// Gets or sets "Collection Name" column value.
        /// </summary>
        [CsvColumn("Collection Name")]
        public string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets "Country" column value.
        /// </summary>
        [CsvColumn("Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets "Postal/Zip Code" column value.
        /// </summary>
        [CsvColumn("Postal/Zip Code")]
        public string PostalZipCode { get; set; }

        /// <summary>
        /// Gets or sets "Primary Contact" column value.
        /// </summary>
        [CsvColumn("Primary Contact")]
        public string PrimaryContact { get; set; }

        /// <summary>
        /// Gets or sets "State/Province" column value.
        /// </summary>
        [CsvColumn("State/Province")]
        public string StateProvince { get; set; }
    }
}