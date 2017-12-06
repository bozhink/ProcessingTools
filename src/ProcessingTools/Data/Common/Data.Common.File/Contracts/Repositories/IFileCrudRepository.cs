namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileCrudRepository<T> : ICrudRepository<T>, IFileSearchableRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
