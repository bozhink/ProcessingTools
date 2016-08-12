namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IMediaTypesRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
