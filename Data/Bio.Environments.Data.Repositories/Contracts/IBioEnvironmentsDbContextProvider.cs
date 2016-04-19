namespace ProcessingTools.Bio.Environments.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioEnvironmentsDbContextProvider : IDbContextProvider<BioEnvironmentsDbContext>
    {
    }
}