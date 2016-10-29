namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Contracts.Data;

    public interface IDbContextInitializer<TContext> : IDatabaseInitializer
        where TContext : DbContext
    {
    }
}