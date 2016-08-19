namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using System.Threading.Tasks;
    using Expressions.Contracts;

    public interface IUpdatableRepository<T> : IRepository<T>
    {
        Task<object> Add(T entity);

        Task<object> Delete(T entity);

        Task<object> Delete(object id);

        Task<long> SaveChanges();

        Task<object> Update(T entity);

        Task<object> Update(object id, IUpdateExpression<T> update);
    }
}
