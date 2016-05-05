namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using Models.Contracts;

    public interface ISimpleGenericContextRepository<TEntity> : IGenericContextRepository<string, int, TEntity>
        where TEntity : IEntity
    {
    }
}
