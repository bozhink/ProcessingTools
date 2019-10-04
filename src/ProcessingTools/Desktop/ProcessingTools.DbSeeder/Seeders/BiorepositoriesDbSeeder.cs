namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Seed.Bio.Biorepositories;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class BiorepositoriesDbSeeder : IBiorepositoriesDbSeeder
    {
        private readonly IBiorepositoriesDataSeeder seeder;

        public BiorepositoriesDbSeeder(IBiorepositoriesDataSeeder seeder)
        {
            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            this.seeder = seeder;
        }

        public async Task Seed()
        {
            await this.seeder.SeedAsync().ConfigureAwait(false);
        }
    }
}
