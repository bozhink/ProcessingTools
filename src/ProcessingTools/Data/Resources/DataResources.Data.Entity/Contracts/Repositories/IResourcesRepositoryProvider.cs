namespace ProcessingTools.DataResources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IResourcesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
