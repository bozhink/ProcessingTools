namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class CitiesDataService : AbstractGeoSynonymisableDataService<ICitiesRepository, ICity, ICitiesFilter, ICitySynonym, ICitySynonymsFilter>, ICitiesDataService
    {
        public CitiesDataService(ICitiesRepository repository)
            : base(repository)
        {
        }
    }
}
