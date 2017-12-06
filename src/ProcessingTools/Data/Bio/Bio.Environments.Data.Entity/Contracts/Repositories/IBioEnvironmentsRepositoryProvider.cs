namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioEnvironmentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
