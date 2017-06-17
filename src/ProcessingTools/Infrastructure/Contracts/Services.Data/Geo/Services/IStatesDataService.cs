namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IStatesDataService : IDataServiceAsync<IState, IStatesFilter>, ISynonymisableDataService<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
