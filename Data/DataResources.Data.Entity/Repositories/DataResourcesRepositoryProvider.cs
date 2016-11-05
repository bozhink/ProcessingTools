namespace ProcessingTools.DataResources.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class DataResourcesRepositoryProvider<T> : IDataResourcesRepositoryProvider<T>
        where T : class
    {
        private readonly IDataResourcesDbContextProvider contextProvider;

        public DataResourcesRepositoryProvider(IDataResourcesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new DataResourcesRepository<T>(this.contextProvider);
        }
    }
}
