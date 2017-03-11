namespace ProcessingTools.Journals.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class JournalsDatabaseInitializer : GenericDbContextInitializer<JournalsDbContext>, IJournalsDatabaseInitializer
    {
        public JournalsDatabaseInitializer(IJournalsDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JournalsDbContext, Configuration>());
        }
    }
}
