namespace ProcessingTools.Bio.Data.Seed.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioDataInitializer : IDbContextInitializer<BioDbContext>
    {
    }
}