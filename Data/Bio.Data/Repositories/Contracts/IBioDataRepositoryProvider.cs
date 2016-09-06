namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IBioDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
