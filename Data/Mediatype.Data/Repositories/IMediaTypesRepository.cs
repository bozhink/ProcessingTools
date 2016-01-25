namespace ProcessingTools.MediaType.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IMediaTypesRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}