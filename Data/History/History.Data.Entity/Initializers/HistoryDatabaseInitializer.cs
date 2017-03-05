namespace ProcessingTools.History.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

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
