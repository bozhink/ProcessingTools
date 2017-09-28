namespace ProcessingTools.Data.Common.File.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IFileSearchableRepository<T> : ISearchableRepository<T>, IFileRepository<T>
    {
    }
}
