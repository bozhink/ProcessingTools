namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface ITaxonomyDbContextProvider : IDbContextProvider<TaxonomyDbContext>
    {
    }
}