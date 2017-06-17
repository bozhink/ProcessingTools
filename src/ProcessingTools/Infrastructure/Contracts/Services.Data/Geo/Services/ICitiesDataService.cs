﻿namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ICitiesDataService : IDataServiceAsync<ICity, ICitiesFilter>, ISynonymisableDataService<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
