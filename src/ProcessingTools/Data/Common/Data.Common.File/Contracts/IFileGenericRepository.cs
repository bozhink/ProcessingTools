namespace ProcessingTools.Data.Common.File.Contracts
{
    public interface IFileGenericRepository<T> : IFileCrudRepository<T>
        where T : class
    {
    }
}
