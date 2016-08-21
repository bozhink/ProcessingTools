namespace ProcessingTools.Data.Common.File.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IFileSearchableRepository<T> : ISearchableRepository<T>, IFileRepository<T>
    {
    }
}
