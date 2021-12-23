// <copyright file="BiotaxonomyMongoDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Seed.Bio.Taxonomy;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Bio taxonomy MongoDB database seeder.
    /// </summary>
    public class BiotaxonomyMongoDbSeeder : GenericDbSeeder<IBiotaxonomyDatabaseInitializer, IBiotaxonomyMongoDatabaseSeeder>, IBiotaxonomyMongoDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiotaxonomyMongoDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IBiotaxonomyDatabaseInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IBiotaxonomyMongoDatabaseSeeder"/>.</param>
        public BiotaxonomyMongoDbSeeder(IBiotaxonomyDatabaseInitializer initializer, IBiotaxonomyMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
