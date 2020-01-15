﻿// <copyright file="GenericDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Abstractions.Seeders
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public abstract class GenericDbSeeder<TInitializer, TSeeder> : IDbSeeder
        where TInitializer : class, IDatabaseInitializer
        where TSeeder : class, IDatabaseSeeder
    {
        private readonly TInitializer initializer;
        private readonly TSeeder seeder;

        protected GenericDbSeeder(TInitializer initializer, TSeeder seeder)
        {
            this.initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
            this.seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
        }

        public virtual async Task Seed()
        {
            await this.initializer.InitializeAsync().ConfigureAwait(false);
            await this.seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}
