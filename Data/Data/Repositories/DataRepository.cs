namespace ProcessingTools.Data.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Repositories.Contracts;

    public class DataRepository<T> : EntityGenericRepository<DataDbContext, T>, IDataRepository<T>
        where T : class
    {
        public DataRepository(IDataDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
