namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;

    public class TaxonomyDbSeeder : ITaxonomyDbSeeder
    {
        private readonly IBioTaxonomyDataInitializer initializer;
        private readonly IBioTaxonomyDataSeeder seeder;

        public TaxonomyDbSeeder(IBioTaxonomyDataInitializer initializer, IBioTaxonomyDataSeeder seeder)
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