namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IBioEnvironmentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
