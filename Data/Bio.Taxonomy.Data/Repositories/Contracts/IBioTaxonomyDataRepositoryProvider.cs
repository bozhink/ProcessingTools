namespace ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioTaxonomyDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
