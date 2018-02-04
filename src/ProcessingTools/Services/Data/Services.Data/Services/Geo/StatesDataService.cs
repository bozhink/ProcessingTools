﻿namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;

    public class StatesDataService : AbstractGeoSynonymisableDataService<IStatesRepository, IState, IStatesFilter, IStateSynonym, IStateSynonymsFilter>, IStatesDataService
    {
        public StatesDataService(IStatesRepository repository)
            : base(repository)
        {
        }
    }
}
