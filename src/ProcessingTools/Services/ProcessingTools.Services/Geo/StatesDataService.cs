namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class StatesDataService : AbstractGeoSynonymisableDataService<IStatesRepository, IState, IStatesFilter, IStateSynonym, IStateSynonymsFilter>, IStatesDataService
    {
        public StatesDataService(IStatesRepository repository)
            : base(repository)
        {
        }
    }
}
