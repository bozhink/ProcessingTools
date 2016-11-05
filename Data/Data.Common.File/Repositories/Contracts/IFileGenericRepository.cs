namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileGenericRepository<T> : ISearchableCountableCrudRepository<T>, IFileCrudRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
