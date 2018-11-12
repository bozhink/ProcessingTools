namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using ProcessingTools.Data.Mongo.Abstractions;

    public interface IBiorepositoriesRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
