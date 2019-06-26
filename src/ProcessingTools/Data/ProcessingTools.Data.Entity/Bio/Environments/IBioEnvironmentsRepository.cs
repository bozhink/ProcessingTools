namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioEnvironmentsRepository<T> : IEntityCrudRepository<T>
        where T : class
    {
    }
}
