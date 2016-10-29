namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ICountableRepository<T> : IRepository<T>
    {
        Task<long> Count();

        Task<long> Count(Expression<Func<T, bool>> filter);
    }
}
