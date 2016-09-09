namespace ProcessingTools.DataResources.Data.Entity
{
    using System;
    using Contracts;

    public class DataResourcesDbContextProvider : IDataResourcesDbContextProvider
    {
        private readonly IDataResourcesDbContextFactory contextFactory;

        public DataResourcesDbContextProvider(IDataResourcesDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public DataResourcesDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
