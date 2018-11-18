namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using ProcessingTools.Data.Contracts;

    public interface IBiorepositoriesRepositoryProvider<T> : ICrudRepositoryProvider<T>
        where T : class
    {
    }
}
