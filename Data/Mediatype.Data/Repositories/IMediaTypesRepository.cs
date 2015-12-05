namespace ProcessingTools.MediaType.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IMediaTypesRepository<T> : IRepository<T>
        where T : class
    {
    }
}