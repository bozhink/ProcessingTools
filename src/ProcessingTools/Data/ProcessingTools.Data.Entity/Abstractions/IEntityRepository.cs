namespace ProcessingTools.Data.Entity.Abstractions
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
