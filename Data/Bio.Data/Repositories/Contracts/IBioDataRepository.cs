namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBioDataRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}