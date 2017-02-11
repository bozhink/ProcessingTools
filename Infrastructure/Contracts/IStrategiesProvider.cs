namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;

    public interface IStrategiesProvider<TStrategy>
        where TStrategy : IStrategy
    {
        IEnumerable<TStrategy> Strategies { get; }
    }
}
