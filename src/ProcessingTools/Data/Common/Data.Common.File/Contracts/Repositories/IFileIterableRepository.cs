namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IFileIterableRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
