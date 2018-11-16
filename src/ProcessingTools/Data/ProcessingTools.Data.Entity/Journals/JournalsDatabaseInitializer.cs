namespace ProcessingTools.Journals.Data.Entity
{
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Journals.Data.Entity.Contracts;

    public class JournalsDatabaseInitializer : DbContextInitializer<JournalsDbContext>, IJournalsDatabaseInitializer
    {
        public JournalsDatabaseInitializer(JournalsDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
