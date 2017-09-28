namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IFileGenericRepository<T> : ICrudRepository<T>, IFileCrudRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
