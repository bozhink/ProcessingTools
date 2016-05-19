namespace ProcessingTools.Geo.Data.Repositories
{
    using System;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Geo.Data.Contracts;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

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

        public IGenericRepository<T> Create()
        {
            return new GeoDataRepository<T>(this.contextProvider);
        }
    }
}