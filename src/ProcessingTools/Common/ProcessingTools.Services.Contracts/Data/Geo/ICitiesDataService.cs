namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface ICitiesDataService : IDataServiceAsync<ICity, ICitiesFilter>, IGeoSynonymisableDataService<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
