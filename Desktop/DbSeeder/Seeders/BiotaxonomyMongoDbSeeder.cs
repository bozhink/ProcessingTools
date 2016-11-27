namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Seeders;

    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;

    public class BiotaxonomyMongoDbSeeder : IBiotaxonomyMongoDbSeeder
    {
        private readonly IBiotaxonomyMongoDatabaseInitializer initializer;
        private readonly IBiotaxonomyMongoDatabaseSeeder seeder;

        public BiotaxonomyMongoDbSeeder(
            IBiotaxonomyMongoDatabaseInitializer initializer,
            IBiotaxonomyMongoDatabaseSeeder seeder)
        {
            if (initializer == null)
            {
                throw new ArgumentNullException(nameof(initializer));
            }

            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            this.initializer = initializer;
            this.seeder = seeder;
        }

        public async Task Seed()
        {
            await this.initializer.Initialize();
            await this.seeder.Seed();
        }
    }
}
