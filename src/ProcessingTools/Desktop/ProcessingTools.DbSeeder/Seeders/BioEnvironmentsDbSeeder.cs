// <copyright file="BioEnvironmentsDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Seed.Bio.Environments;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Bio Environments database seeder.
    /// </summary>
    public class BioEnvironmentsDbSeeder : GenericDbSeeder<IBioEnvironmentsDataInitializer, IBioEnvironmentsDataSeeder>, IBioEnvironmentsDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioEnvironmentsDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IBioEnvironmentsDataInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IBioEnvironmentsDataSeeder"/>.</param>
        public BioEnvironmentsDbSeeder(IBioEnvironmentsDataInitializer initializer, IBioEnvironmentsDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
