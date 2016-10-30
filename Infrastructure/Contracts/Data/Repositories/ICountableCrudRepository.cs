namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ICountableCrudRepository<T> : ICountableRepository<T>, ICrudRepository<T>
    {
    }
}
