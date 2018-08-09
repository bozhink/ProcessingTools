namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IFileGenericRepository<T> : ICrudRepository<T>, IIterableRepository<T>
        where T : class
    {
    }
}
