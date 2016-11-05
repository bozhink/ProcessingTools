namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioTaxonomyDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}