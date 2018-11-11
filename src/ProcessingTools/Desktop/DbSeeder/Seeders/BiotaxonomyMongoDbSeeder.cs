namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;

    public class BiotaxonomyMongoDbSeeder : GenericDbSeeder<IBiotaxonomyDatabaseInitializer, IBiotaxonomyMongoDatabaseSeeder>, IBiotaxonomyMongoDbSeeder
    {
        public BiotaxonomyMongoDbSeeder(IBiotaxonomyDatabaseInitializer initializer, IBiotaxonomyMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
