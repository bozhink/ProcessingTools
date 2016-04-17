namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data.Seed.Contracts;

    public class BioDbSeeder : IBioDbSeeder
    {
        private IBioDataSeeder seeder;

        public BioDbSeeder(IBioDataSeeder seeder)
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