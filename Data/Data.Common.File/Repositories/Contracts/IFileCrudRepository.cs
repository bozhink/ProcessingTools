namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IFileCrudRepository<T> : ICrudRepository<T>, IFileSearchableRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
