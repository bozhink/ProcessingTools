// <copyright file="IInitializeModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace ProcessingTools.Contracts.Services.Models.Admin.Databases
{
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
