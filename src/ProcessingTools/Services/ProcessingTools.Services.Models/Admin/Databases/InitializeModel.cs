// <copyright file="InitializeModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Admin.Databases
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Services.Models.Admin.Databases;

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
