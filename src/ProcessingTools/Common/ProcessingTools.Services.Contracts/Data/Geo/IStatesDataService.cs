namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IStatesDataService : IDataServiceAsync<IState, IStatesFilter>, IGeoSynonymisableDataService<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
