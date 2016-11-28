namespace ProcessingTools.DataResources.Data.Entity.Contracts
{
    using System.Data.Entity.Infrastructure;

    public interface IResourcesDbContextFactory : IDbContextFactory<ResourcesDbContext>
    {
        string ConnectionString { get; set; }
    }
}
