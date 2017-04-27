namespace ProcessingTools.Journals.Data.Entity.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IJournalsDatabaseInitializer : IDbContextInitializer<JournalsDbContext>
    {
    }
}
