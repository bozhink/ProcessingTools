namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts;

    public interface IBioTaxonomyDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
