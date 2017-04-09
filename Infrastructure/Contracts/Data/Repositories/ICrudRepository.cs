namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICrudRepository<T> : IAsyncRepository<T>, IUpdatableRepository<T>
    {
    }
}
