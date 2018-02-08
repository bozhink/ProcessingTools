namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class ProvincesDataService : AbstractGeoSynonymisableDataService<IProvincesRepository, IProvince, IProvincesFilter, IProvinceSynonym, IProvinceSynonymsFilter>, IProvincesDataService
    {
        public ProvincesDataService(IProvincesRepository repository)
            : base(repository)
        {
        }
    }
}
