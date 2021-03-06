﻿namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;

    public class GeoNamesDataService : AbstractGeoMultiDataService<IGeoNamesRepository, IGeoName, ITextFilter>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoNamesRepository repository)
            : base(repository)
        {
        }
    }
}
