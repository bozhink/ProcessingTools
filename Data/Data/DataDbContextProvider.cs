namespace ProcessingTools.Data
{
    using System;
    using Contracts;

    public class DataDbContextProvider : IDataDbContextProvider
    {
        private IDataDbContextFactory contextFactory;

        public DataDbContextProvider(IDataDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public DataDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}