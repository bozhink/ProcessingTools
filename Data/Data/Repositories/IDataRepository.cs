namespace ProcessingTools.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IDataRepository<T> : IRepository<T>
        where T : class
    {
    }
}