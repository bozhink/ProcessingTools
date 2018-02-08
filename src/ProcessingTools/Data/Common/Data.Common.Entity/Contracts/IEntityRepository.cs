namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
