namespace ProcessingTools.MediaType.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IMediaTypesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
