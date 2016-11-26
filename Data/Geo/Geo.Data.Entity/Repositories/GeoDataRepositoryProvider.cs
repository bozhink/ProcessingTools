namespace ProcessingTools.Geo.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class GeoDataRepositoryProvider<T> : IGeoDataRepositoryProvider<T>
        where T : class
    {
        private IGeoDbContextProvider contextProvider;

        public GeoDataRepositoryProvider(IGeoDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new GeoDataRepository<T>(this.contextProvider);
        }
    }
}
