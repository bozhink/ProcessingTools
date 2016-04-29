namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data.Seed.Contracts;

    public class BioDbSeeder : IBioDbSeeder
    {
        private readonly IBioDataInitializer initializer;
        private readonly IBioDataSeeder seeder;

        public BioDbSeeder(IBioDataInitializer initializer, IBioDataSeeder seeder)
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