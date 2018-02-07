// <copyright file="IDatabaseSeeder.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to objects for creation and population with initial data (seed)
    /// of a database context.
    /// </summary>
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Populates the database context with initial data (seed).
        /// </summary>
        /// <returns>Custom response.</returns>
        Task<object> SeedAsync();
    }
}
