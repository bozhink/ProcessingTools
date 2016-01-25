namespace ProcessingTools.Data.Common.UnitOfWork.Contracts
{
    using Repositories.Contracts;

    public interface IEfUnitOfWork
    {
        IEfRepository<T> Get<T>()
            where T : class;

        int SaveChanges();
    }
}
