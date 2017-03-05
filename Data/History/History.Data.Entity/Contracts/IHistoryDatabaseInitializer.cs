namespace ProcessingTools.History.Data.Entity.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IHistoryDatabaseInitializer : IDbContextInitializer<HistoryDbContext>
    {
    }
}
