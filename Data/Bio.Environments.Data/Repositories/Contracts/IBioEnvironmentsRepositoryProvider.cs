namespace ProcessingTools.Bio.Environments.Data.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IBioEnvironmentsRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
