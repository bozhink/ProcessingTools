namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileIterableRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
