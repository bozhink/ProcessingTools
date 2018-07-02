// <copyright file="InitializeModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Admin.Databases
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Services.Models.Contracts.Admin.Databases;

    /// <summary>
    /// Initialize model.
    /// </summary>
    public class InitializeModel : IInitializeModel
    {
        /// <inheritdoc/>
        public int NumberOfDatabases { get; set; }

        /// <inheritdoc/>
        public bool Success { get; set; }

        /// <inheritdoc/>
        public ICollection<Exception> Exceptions { get; set; } = new HashSet<Exception>();
    }
}
