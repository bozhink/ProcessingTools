namespace ProcessingTools.Bio.Taxonomy.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface ITaxonomyDbContextFactory : IDbContextFactory<TaxonomyDbContext>
    {
        string ConnectionString { get; set; }
    }
}