namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;

    public class TaxonomyDbSeeder : ITaxonomyDbSeeder
    {
        private IBioTaxonomyDataSeeder seeder;

        public TaxonomyDbSeeder(IBioTaxonomyDataSeeder seeder)
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