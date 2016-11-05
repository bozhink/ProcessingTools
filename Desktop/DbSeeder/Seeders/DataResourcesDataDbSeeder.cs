namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Seed.Contracts;

    public class DataResourcesDataDbSeeder : IDataResourcesDataDbSeeder
    {
        private readonly IDataResourcesDatabaseInitializer initializer;
        private readonly IDataResourcesDataSeeder seeder;

        public DataResourcesDataDbSeeder(IDataResourcesDatabaseInitializer initializer, IDataResourcesDataSeeder seeder)
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