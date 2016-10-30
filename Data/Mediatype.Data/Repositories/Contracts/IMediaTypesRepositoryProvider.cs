namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IMediaTypesRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
