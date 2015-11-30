namespace ProcessingTools.MediaType.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IMediaTypesRepository<T> : ProcessingTools.Data.Common.Repositories.IMediaTypesRepository<T>
        where T : class
    {
    }
}