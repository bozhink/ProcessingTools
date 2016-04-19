namespace ProcessingTools.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDataDbContextProvider : IDbContextProvider<DataDbContext>
    {
    }
}