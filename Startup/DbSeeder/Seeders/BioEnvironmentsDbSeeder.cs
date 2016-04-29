namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Environments.Data.Seed.Contracts;

    public class BioEnvironmentsDbSeeder : IBioEnvironmentsDbSeeder
    {
        private readonly IBioEnvironmentsInitializer initializer;
        private readonly IBioEnvironmentsDataSeeder seeder;

        public BioEnvironmentsDbSeeder(IBioEnvironmentsInitializer initializer, IBioEnvironmentsDataSeeder seeder)
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