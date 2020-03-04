// <copyright file="GenericDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Abstractions.Seeders
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Generic database seeder.
    /// </summary>
    /// <typeparam name="TInitializer">Type of the database initializer.</typeparam>
    /// <typeparam name="TSeeder">Type of the database seeder.</typeparam>
    public abstract class GenericDbSeeder<TInitializer, TSeeder> : IDbSeeder
        where TInitializer : class, IDatabaseInitializer
        where TSeeder : class, IDatabaseSeeder
    {
        private readonly TInitializer initializer;
        private readonly TSeeder seeder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDbSeeder{TInitializer, TSeeder}"/> class.
        /// </summary>
        /// <param name="initializer">Instance of the database initializer.</param>
        /// <param name="seeder">Instance of the seeder.</param>
        protected GenericDbSeeder(TInitializer initializer, TSeeder seeder)
        {
            this.initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
            this.seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
        }

        /// <inheritdoc/>
        public virtual async Task SeedAsync()
        {
            await this.initializer.InitializeAsync().ConfigureAwait(false);
            await this.seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}
