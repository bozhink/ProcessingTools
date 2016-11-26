namespace ProcessingTools.DataResources.Data.Entity.Providers
{
    using System;
    using Contracts;

    public class ResourcesDbContextProvider : IResourcesDbContextProvider
    {
        private readonly IResourcesDbContextFactory contextFactory;

        public ResourcesDbContextProvider(IResourcesDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public ResourcesDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
