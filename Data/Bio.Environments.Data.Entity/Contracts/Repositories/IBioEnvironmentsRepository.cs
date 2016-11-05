namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioEnvironmentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}