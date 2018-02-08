namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class CitiesDataService : AbstractGeoSynonymisableDataService<ICitiesRepository, ICity, ICitiesFilter, ICitySynonym, ICitySynonymsFilter>, ICitiesDataService
    {
        public CitiesDataService(ICitiesRepository repository)
            : base(repository)
        {
        }
    }
}
