namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IBioTaxonomyDbContextFactory : IDbContextFactory<BioTaxonomyDbContext>
    {
        string ConnectionString { get; set; }
    }
}
