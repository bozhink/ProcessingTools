namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICountableCrudRepository<T> : ICountableRepository<T>, ICrudRepository<T>
    {
    }
}
