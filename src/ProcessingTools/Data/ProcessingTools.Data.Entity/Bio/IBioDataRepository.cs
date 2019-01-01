namespace ProcessingTools.Data.Entity.Bio
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
