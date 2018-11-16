namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IResourcesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
