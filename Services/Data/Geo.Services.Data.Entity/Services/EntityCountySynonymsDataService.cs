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

    public class EntityCountySynonymsDataService : AbstractGeoDataService<CountySynonym, ICountySynonym, ICountySynonymsFilter>, IEntityCountySynonymsDataService
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

        public override async Task<object> UpdateAsync(ICountySynonym model)
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

        protected override IQueryable<CountySynonym> GetQuery(ICountySynonymsFilter filter)
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
