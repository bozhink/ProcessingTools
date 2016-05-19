namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBioTaxonomyDataRepositoryProvider<T> : IGenericRepositoryProvider<IGenericRepository<T>, T>
        where T : class
    {
    }
}