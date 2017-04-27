namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFileSearchableRepository<T> : ISearchableRepository<T>, IFileRepository<T>
    {
    }
}
