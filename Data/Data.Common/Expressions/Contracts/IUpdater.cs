namespace ProcessingTools.Data.Common.Expressions.Contracts
{
    using System.Threading.Tasks;

    public interface IUpdater<T>
    {
        IUpdateExpression<T> UpdateExpression { get; }

        Task Invoke(T obj);
    }
}
