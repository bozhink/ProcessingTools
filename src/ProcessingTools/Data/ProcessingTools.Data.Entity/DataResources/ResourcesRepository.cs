namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class ResourcesRepository<T> : EntityRepository<ResourcesDbContext, T>, IResourcesRepository<T>
        where T : class
    {
        public ResourcesRepository(ResourcesDbContext context)
            : base(context)
        {
        }
    }
}
