namespace ProcessingTools.History.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IHistoryDbContextFactory : IDbContextFactory<HistoryDbContext>
    {
    }
}
