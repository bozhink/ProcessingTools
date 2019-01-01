namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
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
