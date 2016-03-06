namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IMediaTypesRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}