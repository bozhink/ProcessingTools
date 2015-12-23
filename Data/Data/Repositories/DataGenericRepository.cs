namespace ProcessingTools.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class DataGenericRepository<T> : EfGenericRepository<IDataDbContext, T>, IDataRepository<T>
        where T : class
    {
        public DataGenericRepository(IDataDbContext context)
            : base(context)
        {
        }
    }
}