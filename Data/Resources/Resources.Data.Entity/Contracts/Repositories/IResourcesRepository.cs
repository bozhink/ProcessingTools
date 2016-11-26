namespace ProcessingTools.Resources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IResourcesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}