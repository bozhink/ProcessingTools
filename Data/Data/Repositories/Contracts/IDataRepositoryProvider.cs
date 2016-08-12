namespace ProcessingTools.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IDataRepositoryProvider<T> : IGenericRepositoryProvider<T>
        where T : class
    {
    }
}
