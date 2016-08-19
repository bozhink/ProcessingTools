namespace ProcessingTools.Data.Common.Expressions.Contracts
{
    using System.Threading.Tasks;

    public interface IUpdater<TEntity, TDbModel>
        where TDbModel : TEntity
    {
        IUpdateExpression<TEntity> UpdateExpression { get; }

        Task Invoke(TDbModel obj);
    }
}
