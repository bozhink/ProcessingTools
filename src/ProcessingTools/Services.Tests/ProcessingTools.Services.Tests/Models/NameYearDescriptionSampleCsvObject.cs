﻿// <copyright file="NameYearDescriptionSampleCsvObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Services.Serialization.Csv;

namespace ProcessingTools.Services.Tests.Models
{
    /// <summary>
    /// Name-Year-Description Sample CSV Object.
    /// </summary>
    [CsvObject]
    internal class NameYearDescriptionSampleCsvObject
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [CsvColumn]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        [CsvColumn]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [CsvColumn]
        public string Description { get; set; }
    }
}
