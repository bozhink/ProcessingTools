namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IFileGenericRepository<T> : ICrudRepository<T>, IFileCrudRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
