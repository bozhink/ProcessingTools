namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using System.Data.Entity;
    using ProcessingTools.Data.Contracts;

    public interface IDbContextInitializer<TContext> : IDatabaseInitializer
        where TContext : DbContext
    {
    }
}
