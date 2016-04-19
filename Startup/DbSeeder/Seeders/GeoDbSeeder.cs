namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDbSeeder : IGeoDbSeeder
    {
        private IGeoDataSeeder seeder;

        public GeoDbSeeder(IGeoDataSeeder seeder)
        {
            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            this.seeder = seeder;
        }

        public async Task Seed()
        {
            await this.seeder.Init();
            await this.seeder.Seed();
        }
    }
}