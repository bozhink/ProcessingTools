namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Contracts;

    public interface IDbContextInitializer<TContext> : IDatabaseInitializer
        where TContext : DbContext
    {
    }
}