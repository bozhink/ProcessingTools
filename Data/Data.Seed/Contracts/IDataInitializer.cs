namespace ProcessingTools.Data.Seed.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDataInitializer : IDbContextInitializer<DataDbContext>
    {
    }
}