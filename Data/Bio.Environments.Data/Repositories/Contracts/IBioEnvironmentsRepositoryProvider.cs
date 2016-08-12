namespace ProcessingTools.Bio.Environments.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBioEnvironmentsRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
