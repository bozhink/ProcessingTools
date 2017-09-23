namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, IGeoSynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
