namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;

    public class CountiesDataService : AbstractGeoSynonymisableDataService<ICountiesRepository, ICounty, ICountiesFilter, ICountySynonym, ICountySynonymsFilter>, ICountiesDataService
    {
        public CountiesDataService(ICountiesRepository repository)
            : base(repository)
        {
        }
    }
}
