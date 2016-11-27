namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Seeders;

    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Seed.Contracts;

    public class DataResourcesDataDbSeeder : IDataResourcesDataDbSeeder
    {
        private readonly IResourcesDatabaseInitializer initializer;
        private readonly IResourcesDataSeeder seeder;

        public DataResourcesDataDbSeeder(IResourcesDatabaseInitializer initializer, IResourcesDataSeeder seeder)
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