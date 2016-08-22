namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IIterableFileRepository<T> : IIterableRepository<T>, IFileRepository<T>
    {
    }
}
