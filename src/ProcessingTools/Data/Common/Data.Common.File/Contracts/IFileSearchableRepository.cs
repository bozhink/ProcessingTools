namespace ProcessingTools.Data.Common.File.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IFileSearchableRepository<T> : ISearchableRepository<T>, IFileRepository<T>
    {
    }
}
