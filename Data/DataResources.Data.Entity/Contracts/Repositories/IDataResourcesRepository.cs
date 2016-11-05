namespace ProcessingTools.DataResources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IDataResourcesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}