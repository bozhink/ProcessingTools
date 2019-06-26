namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioTaxonomyRepository<T> : EntityRepository<BioTaxonomyDbContext, T>, IBioTaxonomyDataRepository<T>
        where T : class
    {
        public BioTaxonomyRepository(BioTaxonomyDbContext context)
            : base(context)
        {
        }
    }
}
