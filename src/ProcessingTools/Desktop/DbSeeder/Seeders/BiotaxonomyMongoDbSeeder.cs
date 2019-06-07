namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Seed.Bio.Taxonomy;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class BiotaxonomyMongoDbSeeder : GenericDbSeeder<IBiotaxonomyDatabaseInitializer, IBiotaxonomyMongoDatabaseSeeder>, IBiotaxonomyMongoDbSeeder
    {
        public BiotaxonomyMongoDbSeeder(IBiotaxonomyDatabaseInitializer initializer, IBiotaxonomyMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
