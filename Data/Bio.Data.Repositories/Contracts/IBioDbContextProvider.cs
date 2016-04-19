namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioDbContextProvider : IDbContextProvider<BioDbContext>
    {
    }
}