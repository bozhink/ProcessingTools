namespace ProcessingTools.Bio.Environments.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioEnvironmentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}