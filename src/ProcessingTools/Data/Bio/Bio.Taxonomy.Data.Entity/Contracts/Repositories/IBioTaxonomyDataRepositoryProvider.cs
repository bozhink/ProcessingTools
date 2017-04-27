namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioTaxonomyDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
