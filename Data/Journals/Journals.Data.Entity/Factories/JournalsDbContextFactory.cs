namespace ProcessingTools.Journals.Data.Entity.Factories
{
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class JournalsDbContextFactory : IJournalsDbContextFactory
    {
        public JournalsDbContext Create()
        {
            return new JournalsDbContext(ConnectionStringsKeys.JournalsDatabaseConnection);
        }
    }
}
