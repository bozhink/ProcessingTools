namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileIterableRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
