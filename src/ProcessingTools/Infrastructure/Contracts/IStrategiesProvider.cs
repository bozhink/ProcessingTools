namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;

    public interface IStrategiesProvider<out TStrategy>
        where TStrategy : IStrategy
    {
        IEnumerable<TStrategy> Strategies { get; }
    }
}
