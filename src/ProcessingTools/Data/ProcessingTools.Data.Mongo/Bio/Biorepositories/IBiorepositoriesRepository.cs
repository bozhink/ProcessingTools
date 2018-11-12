namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using Abstractions;

    public interface IBiorepositoriesRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
