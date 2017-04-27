namespace ProcessingTools.Journals.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IJournalsDbContextFactory : IDbContextFactory<JournalsDbContext>
    {
    }
}
