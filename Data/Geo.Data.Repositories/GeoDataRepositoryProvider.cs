namespace ProcessingTools.Geo.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

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