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

    public class EntityProvinceSynonymsDataService : AbstractGeoDataSynonymsService<ProvinceSynonym, IProvinceSynonym, IProvinceSynonymsFilter>, IEntityProvinceSynonymsDataService
    {
        public EntityProvinceSynonymsDataService(IGeoRepository<ProvinceSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<ProvinceSynonym, IProvinceSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.ProvinceSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.ProvinceId
        };

        protected override Func<IProvinceSynonym, ProvinceSynonym> MapModelToEntity => s => new ProvinceSynonym
        {
            Id = s.Id,
            Name = s.Name,
            ProvinceId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
