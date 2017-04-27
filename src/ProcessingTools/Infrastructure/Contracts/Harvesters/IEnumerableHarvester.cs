namespace ProcessingTools.Contracts.Harvesters
{
    using System.Collections.Generic;

    public interface IEnumerableHarvester<TContext, TResult> : IHarvester<TContext, IEnumerable<TResult>>
    {
    }
}
