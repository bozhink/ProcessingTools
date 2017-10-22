namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Data.Geo;

    public class ContinentsDataService : AbstractGeoSynonymisableDataService<IContinentsRepository, IContinent, IContinentsFilter, IContinentSynonym, IContinentSynonymsFilter>, IContinentsDataService
    {
        public ContinentsDataService(IContinentsRepository repository)
            : base(repository)
        {
        }
    }
}
