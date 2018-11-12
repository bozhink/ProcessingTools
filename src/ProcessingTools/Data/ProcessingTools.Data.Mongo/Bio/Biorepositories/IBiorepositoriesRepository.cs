namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using Common.Contracts;

    public interface IBiorepositoriesRepository<T> : IMongoGenericRepository<T>
        where T : class
    {
    }
}
