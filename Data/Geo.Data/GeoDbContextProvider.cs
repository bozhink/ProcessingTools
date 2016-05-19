namespace ProcessingTools.Geo.Data
{
    using System;
    using Contracts;

    public class GeoDbContextProvider : IGeoDbContextProvider
    {
        private readonly IGeoDbContextFactory contextFactory;

        public GeoDbContextProvider(IGeoDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public GeoDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}