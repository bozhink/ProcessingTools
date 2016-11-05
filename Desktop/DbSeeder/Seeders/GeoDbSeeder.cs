namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDbSeeder : IGeoDbSeeder
    {
        private readonly IGeoDataInitializer initializer;
        private readonly IGeoDataSeeder seeder;

        public GeoDbSeeder(IGeoDataInitializer initializer, IGeoDataSeeder seeder)
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
