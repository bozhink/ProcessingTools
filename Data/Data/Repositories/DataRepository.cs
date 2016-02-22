namespace ProcessingTools.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories.Factories;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Repositories.Contracts;

    public class DataRepository<T> : EfGenericRepository<IDataDbContext, T>, IDataRepository<T>
        where T : class
    {
        public DataRepository(IDataDbContext context)
            : base(context)
        {
        }
    }
}
