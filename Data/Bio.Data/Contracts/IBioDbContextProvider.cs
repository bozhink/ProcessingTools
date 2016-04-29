namespace ProcessingTools.Bio.Data.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioDbContextProvider : IDbContextProvider<BioDbContext>
    {
    }
}