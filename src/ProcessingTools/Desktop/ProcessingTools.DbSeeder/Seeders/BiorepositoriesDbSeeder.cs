// <copyright file="BiorepositoriesDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Seed.Bio.Biorepositories;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Biorepositories database seeder.
    /// </summary>
    public class BiorepositoriesDbSeeder : IBiorepositoriesDbSeeder
    {
        private readonly IBiorepositoriesDataSeeder seeder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesDbSeeder"/> class.
        /// </summary>
        /// <param name="seeder">Instance of <see cref="IBiorepositoriesDataSeeder"/>.</param>
        public BiorepositoriesDbSeeder(IBiorepositoriesDataSeeder seeder)
        {
            this.seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
        }

        /// <inheritdoc/>
        public async Task SeedAsync()
        {
            await this.seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}
