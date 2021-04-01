// <copyright file="BioDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio;
    using ProcessingTools.Data.Seed.Bio;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Bio database seeder.
    /// </summary>
    public class BioDbSeeder : GenericDbSeeder<IBioDataInitializer, IBioDataSeeder>, IBioDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IBioDataInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IBioDataSeeder"/>.</param>
        public BioDbSeeder(IBioDataInitializer initializer, IBioDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
