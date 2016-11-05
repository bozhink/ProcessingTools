namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioTaxonomyDbContext : IDbContext
    {
        IDbSet<BlackListEntity> BlackListedItems { get; set; }

        IDbSet<TaxonName> TaxonNames { get; set; }

        IDbSet<TaxonRank> TaxonRanks { get; set; }
    }
}
