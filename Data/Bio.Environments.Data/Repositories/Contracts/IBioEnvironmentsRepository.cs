namespace ProcessingTools.Bio.Environments.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioEnvironmentsRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}