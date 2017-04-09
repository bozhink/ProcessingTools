namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Expressions;

    public interface IUpdatableRepository<T> : IRepository<T>
    {
        Task<object> Update(object id, IUpdateExpression<T> update);
    }
}
