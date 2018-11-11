// <copyright file="NameYearDescriptionSampleCsvObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Models
{
    using ProcessingTools.Common.Code.Serialization.Csv;

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
