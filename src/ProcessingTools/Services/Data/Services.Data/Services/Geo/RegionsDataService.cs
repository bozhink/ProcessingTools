﻿namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;

    public class RegionsDataService : AbstractGeoSynonymisableDataService<IRegionsRepository, IRegion, IRegionsFilter, IRegionSynonym, IRegionSynonymsFilter>, IRegionsDataService
    {
        public RegionsDataService(IRegionsRepository repository)
            : base(repository)
        {
        }
    }
}
