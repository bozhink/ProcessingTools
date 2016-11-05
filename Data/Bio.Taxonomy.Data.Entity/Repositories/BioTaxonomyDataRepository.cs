namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BioTaxonomyDataRepository<T> : EntityGenericRepository<BioTaxonomyDbContext, T>, IBioTaxonomyDataRepository<T>
        where T : class
    {
        public BioTaxonomyDataRepository(IBioTaxonomyDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
