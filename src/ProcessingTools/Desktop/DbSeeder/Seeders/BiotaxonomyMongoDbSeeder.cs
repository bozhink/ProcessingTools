namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;

    public class BiotaxonomyMongoDbSeeder : GenericDbSeeder<IBiotaxonomyMongoDatabaseInitializer, IBiotaxonomyMongoDatabaseSeeder>, IBiotaxonomyMongoDbSeeder
    {
        public BiotaxonomyMongoDbSeeder(IBiotaxonomyMongoDatabaseInitializer initializer, IBiotaxonomyMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
