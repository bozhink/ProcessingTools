namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class StatesDataService : AbstractGeoSynonymisableDataService<IStatesRepository, IState, IStatesFilter, IStateSynonym, IStateSynonymsFilter>, IStatesDataService
    {
        public StatesDataService(IStatesRepository repository)
            : base(repository)
        {
        }
    }
}
