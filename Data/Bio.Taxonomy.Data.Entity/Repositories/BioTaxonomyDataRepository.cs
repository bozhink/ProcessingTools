namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using ProcessingTools.Bio.Taxonomy.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
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