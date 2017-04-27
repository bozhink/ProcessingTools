namespace ProcessingTools.History.Data.Entity.Factories
{
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class HistoryDbContextFactory : IHistoryDbContextFactory
    {
        public HistoryDbContext Create()
        {
            return new HistoryDbContext(ConnectionStringsKeys.HistoryDatabaseConnection);
        }
    }
}
