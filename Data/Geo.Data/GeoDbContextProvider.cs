namespace ProcessingTools.Geo.Data
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