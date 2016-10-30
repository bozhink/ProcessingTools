namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface ICountableRepository<T> : IRepository<T>
    {
        Task<long> Count();

        Task<long> Count(Expression<Func<T, bool>> filter);
    }
}
