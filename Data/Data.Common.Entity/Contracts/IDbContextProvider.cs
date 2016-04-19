namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Contracts;

    public interface IDbContextProvider<TContext> : IDatabaseProvider<TContext>
        where TContext : DbContext
    {
    }
}