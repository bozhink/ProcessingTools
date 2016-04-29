namespace ProcessingTools.Data
{
    using Contracts;

    public class DataDbContextProvider : IDataDbContextProvider
    {
        public DataDbContext Create()
        {
            return DataDbContext.Create();
        }
    }
}