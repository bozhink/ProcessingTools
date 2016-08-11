namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ICountableRepository<TEntity>
    {
        Task<long> Count();

        Task<long> Count(Expression<Func<TEntity, bool>> filter);
    }
}
