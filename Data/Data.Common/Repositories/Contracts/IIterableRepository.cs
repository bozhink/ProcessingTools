namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IIterableRepository<TEntity> : IRepository<TEntity>
    {
        Task<IQueryable<TEntity>> All();
    }
}
