namespace ProcessingTools.Bio.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts;

    public interface IBioDataRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
