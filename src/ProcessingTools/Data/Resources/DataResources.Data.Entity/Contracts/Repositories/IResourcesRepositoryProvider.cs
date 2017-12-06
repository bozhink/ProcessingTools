namespace ProcessingTools.DataResources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IResourcesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
