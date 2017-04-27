namespace ProcessingTools.Bio.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
