// <copyright file="IDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data
{
    using System.Threading.Tasks;

    /// <summary>
    /// Database initializer.
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// Initializes database.
        /// </summary>
        /// <returns>Result of operation.</returns>
        Task<object> InitializeAsync();
    }
}
