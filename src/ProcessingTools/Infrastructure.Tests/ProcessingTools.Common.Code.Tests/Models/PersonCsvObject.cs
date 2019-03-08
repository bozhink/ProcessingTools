﻿// <copyright file="PersonCsvObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Models
{
    using ProcessingTools.Common.Code.Serialization.Csv;

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