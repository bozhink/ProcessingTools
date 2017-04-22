namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Entity.Abstractions;
    using ProcessingTools.Geo.Services.Data.Entity.Contracts.Services;

    public class EntityContinentSynonymsDataService : AbstractGeoDataService<ContinentSynonym, IContinentSynonym, IContinentSynonymsFilter>, IEntityContinentSynonymsDataService
    {
        public EntityContinentSynonymsDataService(IGeoRepository<ContinentSynonym> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        protected override Func<ContinentSynonym, IContinentSynonym> MapEntityToModel => s => new ProcessingTools.Geo.Services.Data.Entity.Models.ContinentSynonym
        {
            Id = s.Id,
            Name = s.Name,
            LanguageCode = s.LanguageCode,
            ParentId = s.ContinentId
        };

        protected override Func<IContinentSynonym, ContinentSynonym> MapModelToEntity => s => new ContinentSynonym
        {
            Id = s.Id,
            Name = s.Name,
            ContinentId = s.ParentId,
            LanguageCode = s.LanguageCode
        };

        public override async Task<object> UpdateAsync(IContinentSynonym model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = this.Repository.Get(model.Id);
            if (entity == null)
            {
                return null;
            }

            entity.LanguageCode = model.LanguageCode;
            entity.Name = model.Name;

            return await base.UpdateEntityAsync(entity);
        }

        protected override IQueryable<ContinentSynonym> GetQuery(IContinentSynonymsFilter filter)
        {
            var query = this.Repository.Queryable();

            if (filter != null)
            {
                query = query.Where(
                    s => (!filter.Id.HasValue || s.Id == filter.Id) &&
                         (!filter.LanguageCode.HasValue || s.LanguageCode == filter.LanguageCode) &&
                         (string.IsNullOrEmpty(filter.Name) || s.Name.ToLower().Contains(filter.Name.ToLower())));
            }

            return query;
        }
    }
}
