namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts;

    public interface IBioEnvironmentsRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
