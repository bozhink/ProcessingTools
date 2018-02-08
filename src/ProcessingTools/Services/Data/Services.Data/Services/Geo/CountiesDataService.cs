namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class CountiesDataService : AbstractGeoSynonymisableDataService<ICountiesRepository, ICounty, ICountiesFilter, ICountySynonym, ICountySynonymsFilter>, ICountiesDataService
    {
        public CountiesDataService(ICountiesRepository repository)
            : base(repository)
        {
        }
    }
}
