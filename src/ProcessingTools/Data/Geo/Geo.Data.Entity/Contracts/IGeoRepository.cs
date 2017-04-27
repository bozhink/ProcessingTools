namespace ProcessingTools.Geo.Data.Entity.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;

    public interface IGeoRepository<T> : IGenericRepository<IGeoDbContext, T>
        where T : class
    {
    }
}
