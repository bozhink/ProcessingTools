namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IFileCrudRepository<T> : ICrudRepository<T>, IFileSearchableRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
