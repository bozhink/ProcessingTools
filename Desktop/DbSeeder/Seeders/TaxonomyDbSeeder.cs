namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts;

    public class TaxonomyDbSeeder : GenericDbSeeder<IBioTaxonomyDataInitializer, IBioTaxonomyDataSeeder>, ITaxonomyDbSeeder
    {
        public TaxonomyDbSeeder(IBioTaxonomyDataInitializer initializer, IBioTaxonomyDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
