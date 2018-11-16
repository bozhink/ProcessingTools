namespace ProcessingTools.Data.Entity.History
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IHistoryDatabaseInitializer : IDbContextInitializer<HistoryDbContext>
    {
    }
}
