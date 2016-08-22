namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IFileIterableRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
