namespace ProcessingTools.Cache.Data.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IGenericValidationCacheDataRepository<TContext> : IGenericRepository<TContext, int, IValidationCacheEntity>
    {
    }
}
