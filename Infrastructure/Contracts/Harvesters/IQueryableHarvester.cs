namespace ProcessingTools.Contracts.Harvesters
{
    using System.Linq;

    public interface IQueryableHarvester<TContext, TResult> : IHarvester<TContext, IQueryable<TResult>>
    {
    }
}
