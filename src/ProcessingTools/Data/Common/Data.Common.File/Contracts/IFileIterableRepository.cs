namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IFileIterableRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
