namespace ProcessingTools.Geo.Services.Data.Entity.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Geo.Data.Entity.Contracts;

    public abstract class AbstractGeoDataSynonymsService<TEntity, TModel, TFilter> : AbstractGeoDataService<TEntity, TModel, TFilter>
        where TEntity : ProcessingTools.Geo.Data.Entity.Models.Synonym, IDataModel
        where TModel : class, ProcessingTools.Contracts.Services.Data.Geo.Models.ISynonym
        where TFilter : IFilter, ISynonymFilter
    {
        public AbstractGeoDataSynonymsService(IGeoRepository<TEntity> repository, IEnvironment environment)
            : base(repository, environment)
        {
        }

        public override async Task<object> UpdateAsync(TModel model)
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

            return await this.UpdateEntityAsync(entity);
        }

        protected override IQueryable<TEntity> GetQuery(TFilter filter)
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
