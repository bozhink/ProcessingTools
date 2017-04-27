namespace ProcessingTools.DataResources.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class ResourcesRepository<T> : EntityGenericRepository<ResourcesDbContext, T>, IResourcesRepository<T>
        where T : class
    {
        public ResourcesRepository(IResourcesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
