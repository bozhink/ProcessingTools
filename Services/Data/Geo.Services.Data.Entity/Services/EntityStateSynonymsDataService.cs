namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityStateSynonymsDataService : AbstractGeoDataSynonymsService<StateSynonym, IStateSynonym, IStateSynonymsFilter>, IEntityStateSynonymsDataService
    {
        public EntityStateSynonymsDataService(IGeoRepository<StateSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<StateSynonym, IStateSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.StateSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.StateId
        };

        protected override Func<IStateSynonym, StateSynonym> MapModelToEntity => s => new StateSynonym
        {
            Id = s.Id,
            Name = s.Name,
            StateId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}