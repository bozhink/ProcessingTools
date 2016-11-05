namespace ProcessingTools.Data.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IDataDbContextFactory : IDbContextFactory<DataDbContext>
    {
        string ConnectionString { get; set; }
    }
}