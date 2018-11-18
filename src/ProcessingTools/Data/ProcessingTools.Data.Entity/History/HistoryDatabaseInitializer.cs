namespace ProcessingTools.Data.Entity.History
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class HistoryDatabaseInitializer : DbContextInitializer<HistoryDbContext>, IHistoryDatabaseInitializer
    {
        public HistoryDatabaseInitializer(HistoryDbContext contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
