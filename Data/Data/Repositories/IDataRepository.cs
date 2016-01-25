namespace ProcessingTools.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IDataRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}