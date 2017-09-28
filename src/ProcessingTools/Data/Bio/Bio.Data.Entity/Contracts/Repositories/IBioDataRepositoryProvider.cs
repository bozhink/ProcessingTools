namespace ProcessingTools.Bio.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IBioDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
