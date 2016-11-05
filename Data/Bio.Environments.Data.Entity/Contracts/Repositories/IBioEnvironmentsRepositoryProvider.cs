namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioEnvironmentsRepositoryProvider<T> : ISearchableCountableCrudRepositoryProvider<T>
        where T : class
    {
    }
}
