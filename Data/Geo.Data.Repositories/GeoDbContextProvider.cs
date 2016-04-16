namespace ProcessingTools.Geo.Data.Repositories
{
    using Contracts;

    public class GeoDbContextProvider : IGeoDbContextProvider
    {
        public GeoDbContext Create()
        {
            return GeoDbContext.Create();
        }
    }
}