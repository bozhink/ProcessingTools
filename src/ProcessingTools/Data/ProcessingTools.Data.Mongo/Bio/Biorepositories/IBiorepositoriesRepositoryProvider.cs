namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using Contracts;

    public interface IBiorepositoriesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
