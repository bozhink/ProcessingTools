namespace ProcessingTools.DataResources.Data.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IDataResourcesGenericRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
