namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileGenericRepository<T> : ISearchableCountableCrudRepository<T>, IFileCrudRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
