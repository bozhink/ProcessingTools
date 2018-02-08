namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;

    public class ProvincesDataService : AbstractGeoSynonymisableDataService<IProvincesRepository, IProvince, IProvincesFilter, IProvinceSynonym, IProvinceSynonymsFilter>, IProvincesDataService
    {
        public ProvincesDataService(IProvincesRepository repository)
            : base(repository)
        {
        }
    }
}
