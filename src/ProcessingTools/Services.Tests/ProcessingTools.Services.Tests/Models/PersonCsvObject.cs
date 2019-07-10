// <copyright file="PersonCsvObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Services.Serialization.Csv;

namespace ProcessingTools.Services.Tests.Models
{
    /// <summary>
    /// Person CSV Object.
    /// </summary>
    [CsvObject]
    internal class PersonCsvObject
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [CsvColumn("first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [CsvColumn("last name")]
        public string LastName { get; set; }
    }
}
