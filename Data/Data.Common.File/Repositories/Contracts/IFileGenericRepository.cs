namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IFileGenericRepository<T> : IGenericRepository<T>, IFileCrudRepository<T>, IFileRepository<T>
        where T : class
    {
    }
}
