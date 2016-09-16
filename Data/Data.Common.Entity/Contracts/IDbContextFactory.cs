namespace ProcessingTools.Data.Common.Entity.Contracts
{
    public interface IDbContextFactory<T> : System.Data.Entity.Infrastructure.IDbContextFactory<T>
        where T : System.Data.Entity.DbContext
    {
        string ConnectionString { get; set; }
    }
}
