// <copyright file="InitializeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Admin.Databases
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initialize response model.
    /// </summary>
    public class InitializeResponseModel
    {
        /// <summary>
        /// Gets or sets the number of databases.
        /// </summary>
        public int NumberOfDatabases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether initialization is successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        public ICollection<Exception> Exceptions { get; set; } = new HashSet<Exception>();
    }
}
