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

    public class EntityCountySynonymsDataService : AbstractGeoDataSynonymsService<CountySynonym, ICountySynonym, ICountySynonymsFilter>, IEntityCountySynonymsDataService
    {
        public EntityCountySynonymsDataService(IGeoRepository<CountySynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<CountySynonym, ICountySynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.CountySynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.CountyId
        };

        protected override Func<ICountySynonym, CountySynonym> MapModelToEntity => s => new CountySynonym
        {
            Id = s.Id,
            Name = s.Name,
            CountyId = s.ParentId,
            LanguageCode = s.LanguageCode
        };
    }
}
