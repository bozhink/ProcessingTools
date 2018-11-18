namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioTaxonomyDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
