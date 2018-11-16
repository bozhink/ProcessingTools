namespace ProcessingTools.Journals.Data.Entity.Contracts
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IJournalsDatabaseInitializer : IDbContextInitializer<JournalsDbContext>
    {
    }
}
