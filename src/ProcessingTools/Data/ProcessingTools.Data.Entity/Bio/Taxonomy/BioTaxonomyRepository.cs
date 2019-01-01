namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioTaxonomyRepository<T> : EntityGenericRepository<BioTaxonomyDbContext, T>, IBioTaxonomyDataRepository<T>
        where T : class
    {
        public BioTaxonomyRepository(IDbContextProvider<BioTaxonomyDbContext> context)
            : base(context)
        {
        }
    }
}
