namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileSearchableRepository<T> : ISearchableRepository<T>, IFileRepository<T>
    {
    }
}
