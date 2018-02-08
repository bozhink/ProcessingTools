namespace ProcessingTools.Geo.Data.Entity.Providers
{
    using System;
    using Contracts;

    public class GeoDbContextProvider : IGeoDbContextProvider
    {
        private readonly IGeoDbContextFactory contextFactory;

        public GeoDbContextProvider(IGeoDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public GeoDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
