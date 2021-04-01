// <copyright file="IDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Contracts.Seeders
{
    using System.Threading.Tasks;

    /// <summary>
    /// Database seeder.
    /// </summary>
    public interface IDbSeeder
    {
        /// <summary>
        /// Perform seeding procedure.
        /// </summary>
        /// <returns>Task.</returns>
        Task SeedAsync();
    }
}
