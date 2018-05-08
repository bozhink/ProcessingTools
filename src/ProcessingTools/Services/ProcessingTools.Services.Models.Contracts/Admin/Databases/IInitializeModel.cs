// <copyright file="IInitializeModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Admin.Databases
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initialize model.
    /// </summary>
    public interface IInitializeModel
    {
        /// <summary>
        /// Gets the number of databases.
        /// </summary>
        int NumberOfDatabases { get; }

        /// <summary>
        /// Gets a value indicating whether initialization is successful.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        ICollection<Exception> Exceptions { get; }
    }
}
