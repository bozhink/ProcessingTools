namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class ContinentsDataService : AbstractGeoSynonymisableDataService<IContinentsRepository, IContinent, IContinentsFilter, IContinentSynonym, IContinentSynonymsFilter>, IContinentsDataService
    {
        public ContinentsDataService(IContinentsRepository repository)
            : base(repository)
        {
        }
    }
}
