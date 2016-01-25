namespace ProcessingTools.Bio.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBioDataRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}