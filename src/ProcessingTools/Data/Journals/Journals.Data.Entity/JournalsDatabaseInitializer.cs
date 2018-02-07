namespace ProcessingTools.Journals.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Abstractions;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Migrations;

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
