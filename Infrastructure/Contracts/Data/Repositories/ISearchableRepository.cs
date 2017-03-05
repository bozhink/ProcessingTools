namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface ISearchableRepository<T> : IRepository<T>, IQueryableRepository<T>, IFiltrableRepository<T>
    {
        Task<T> Get(object id);
    }
}
