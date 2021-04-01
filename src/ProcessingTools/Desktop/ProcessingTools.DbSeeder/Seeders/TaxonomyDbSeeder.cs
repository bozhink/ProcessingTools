// <copyright file="TaxonomyDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio.Taxonomy;
    using ProcessingTools.Data.Seed.Bio.Taxonomy;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Bio taxonomy data seeder.
    /// </summary>
    public class TaxonomyDbSeeder : GenericDbSeeder<IBioTaxonomyDataInitializer, IBioTaxonomyDataSeeder>, ITaxonomyDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IBioTaxonomyDataInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IBioTaxonomyDataSeeder"/>.</param>
        public TaxonomyDbSeeder(IBioTaxonomyDataInitializer initializer, IBioTaxonomyDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
