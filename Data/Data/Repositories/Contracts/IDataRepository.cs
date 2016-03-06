namespace ProcessingTools.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IDataRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}