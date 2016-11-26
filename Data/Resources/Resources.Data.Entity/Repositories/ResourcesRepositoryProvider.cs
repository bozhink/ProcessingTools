namespace ProcessingTools.Resources.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

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

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new ResourcesRepository<T>(this.contextProvider);
        }
    }
}
