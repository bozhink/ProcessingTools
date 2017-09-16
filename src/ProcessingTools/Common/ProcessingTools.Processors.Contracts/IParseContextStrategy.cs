namespace ProcessingTools.Contracts
{
    public interface IParseContextStrategy<TContext, TResult> : IContextParser<TContext, TResult>, IStrategy
    {
    }
}
