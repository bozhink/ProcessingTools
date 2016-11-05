namespace ProcessingTools.Data.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDataInitializer : IDbContextInitializer<DataDbContext>
    {
    }
}