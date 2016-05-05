namespace ProcessingTools.Bio.Taxonomy.Data.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioTaxonomyDbContextProvider : IDbContextProvider<BioTaxonomyDbContext>
    {
    }
}