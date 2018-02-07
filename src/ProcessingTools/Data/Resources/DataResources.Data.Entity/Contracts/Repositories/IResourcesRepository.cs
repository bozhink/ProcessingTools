namespace ProcessingTools.DataResources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IResourcesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
