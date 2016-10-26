namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFullTextSearchableRepository<T> : IRepository<T>
    {
        Task<IQueryable<T>> SearchText(string text);
    }
}
