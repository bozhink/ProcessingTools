namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioTaxonomyDataRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
