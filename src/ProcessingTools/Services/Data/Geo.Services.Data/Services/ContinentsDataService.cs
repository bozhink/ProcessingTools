namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;

    public class ContinentsDataService : AbstractGeoSynonymisableDataService<IContinentsRepository, IContinent, IContinentsFilter, IContinentSynonym, IContinentSynonymsFilter>, IContinentsDataService
    {
        public ContinentsDataService(IContinentsRepository repository)
            : base(repository)
        {
        }
    }
}
