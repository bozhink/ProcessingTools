namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using ProcessingTools.Data.Mongo.Abstractions;

    public interface IBiorepositoriesRepository<T> : IMongoCrudRepository<T>
        where T : class
    {
    }
}
