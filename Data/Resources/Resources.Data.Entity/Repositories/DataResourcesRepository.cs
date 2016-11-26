namespace ProcessingTools.DataResources.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class DataResourcesRepository<T> : EntityGenericRepository<DataResourcesDbContext, T>, IDataResourcesRepository<T>
        where T : class
    {
        public DataResourcesRepository(IDataResourcesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
