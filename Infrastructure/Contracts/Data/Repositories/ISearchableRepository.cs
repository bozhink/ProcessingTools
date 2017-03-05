namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface ISearchableRepository<T> : IRepository<T>, IQueryableRepository<T>, IFirstFilterableRepository<T>, ISelectableByIdRepository<T>
    {
    }
}
