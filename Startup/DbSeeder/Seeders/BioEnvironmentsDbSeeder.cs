namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Environments.Data.Seed.Contracts;

    public class BioEnvironmentsDbSeeder : IBioEnvironmentsDbSeeder
    {
        private IBioEnvironmentsDataSeeder seeder;

        public BioEnvironmentsDbSeeder(IBioEnvironmentsDataSeeder seeder)
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