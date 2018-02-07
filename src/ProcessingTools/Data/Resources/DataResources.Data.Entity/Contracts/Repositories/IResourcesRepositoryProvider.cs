namespace ProcessingTools.DataResources.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts;

    public interface IResourcesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
