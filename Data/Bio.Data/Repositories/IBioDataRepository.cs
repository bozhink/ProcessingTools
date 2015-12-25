namespace ProcessingTools.Bio.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBioDataRepository<T> : IRepository<T>
        where T : class
    {
    }
}