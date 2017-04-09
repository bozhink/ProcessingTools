using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Data.Repositories
{
    public interface IAsyncRepository<T> : IRepository<T>
    {
        Task<long> Count();

        Task<long> Count(Expression<Func<T, bool>> filter);

        Task<object> Add(T entity);

        Task<object> Delete(object id);

        Task<object> Update(T entity);
    }
}
