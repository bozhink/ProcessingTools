namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioTaxonomyDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
