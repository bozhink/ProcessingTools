namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class ResourcesRepository<T> : EntityGenericRepository<ResourcesDbContext, T>, IResourcesRepository<T>
        where T : class
    {
        public ResourcesRepository(IDbContextProvider<ResourcesDbContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
