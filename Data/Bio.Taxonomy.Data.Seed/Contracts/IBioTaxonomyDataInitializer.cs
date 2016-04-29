namespace ProcessingTools.Bio.Taxonomy.Data.Seed.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioTaxonomyDataInitializer : IDbContextInitializer<TaxonomyDbContext>
    {
    }
}