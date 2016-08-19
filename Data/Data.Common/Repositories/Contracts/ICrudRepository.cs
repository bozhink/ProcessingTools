namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Threading.Tasks;
    using Expressions.Contracts;

    public interface ICrudRepository<TEntity> : IUpdatableRepository<TEntity>, IRepository<TEntity>
    {

        Task<TEntity> Get(object id);
        
    }
}
