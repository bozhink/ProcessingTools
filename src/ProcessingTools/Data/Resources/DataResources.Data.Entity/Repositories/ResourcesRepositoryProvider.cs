namespace ProcessingTools.DataResources.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Contracts;

    public class ResourcesRepositoryProvider<T> : IResourcesRepositoryProvider<T>
        where T : class
    {
        private readonly IResourcesDbContextProvider contextProvider;

        public ResourcesRepositoryProvider(IResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ICrudRepository<T> Create()
        {
            return new ResourcesRepository<T>(this.contextProvider);
        }
    }
}
