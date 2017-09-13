namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Seeders;

    using ProcessingTools.Bio.Biorepositories.Data.Seed.Contracts;

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