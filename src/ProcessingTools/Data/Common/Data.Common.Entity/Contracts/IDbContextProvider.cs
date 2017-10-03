namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Contracts;

    public interface IDbContextProvider<out TContext> : IDatabaseProvider<TContext>
        where TContext : DbContext
    {
    }
}
