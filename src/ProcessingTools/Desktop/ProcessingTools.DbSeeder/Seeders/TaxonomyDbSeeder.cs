namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio.Taxonomy;
    using ProcessingTools.Data.Seed.Bio.Taxonomy;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class TaxonomyDbSeeder : GenericDbSeeder<IBioTaxonomyDataInitializer, IBioTaxonomyDataSeeder>, ITaxonomyDbSeeder
    {
        public TaxonomyDbSeeder(IBioTaxonomyDataInitializer initializer, IBioTaxonomyDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
