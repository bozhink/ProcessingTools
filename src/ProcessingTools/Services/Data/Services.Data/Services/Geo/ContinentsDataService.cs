﻿namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;

    public class ContinentsDataService : AbstractGeoSynonymisableDataService<IContinentsRepository, IContinent, IContinentsFilter, IContinentSynonym, IContinentSynonymsFilter>, IContinentsDataService
    {
        public ContinentsDataService(IContinentsRepository repository)
            : base(repository)
        {
        }
    }
}
