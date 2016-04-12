namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IMediaTypesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}