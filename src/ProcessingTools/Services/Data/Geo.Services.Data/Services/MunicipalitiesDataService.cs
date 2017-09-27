namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;

    public class MunicipalitiesDataService : AbstractGeoSynonymisableDataService<IMunicipalitiesRepository, IMunicipality, IMunicipalitiesFilter, IMunicipalitySynonym, IMunicipalitySynonymsFilter>, IMunicipalitiesDataService
    {
        public MunicipalitiesDataService(IMunicipalitiesRepository repository)
            : base(repository)
        {
        }
    }
}
