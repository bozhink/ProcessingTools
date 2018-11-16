namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioEnvironmentsRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
