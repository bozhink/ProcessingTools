namespace ProcessingTools.History.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Abstractions;
    using ProcessingTools.History.Data.Entity.Contracts;
    using ProcessingTools.History.Data.Entity.Migrations;

    public class HistoryDatabaseInitializer : GenericDbContextInitializer<HistoryDbContext>, IHistoryDatabaseInitializer
    {
        public HistoryDatabaseInitializer(IHistoryDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HistoryDbContext, Configuration>());
        }
    }
}
