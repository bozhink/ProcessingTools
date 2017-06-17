namespace ProcessingTools.Geo.Services.Data.Entity.Abstractions
{
    using System.Linq;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Models;

    public abstract partial class AbstractGeoSynonymisableRepository<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IDataServiceAsync<TModel, TFilter>, ISynonymisableDataService<TModel, TSynonymModel, TSynonymFilter>
        where TEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Geo.Data.Entity.Models.ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentifiable, ProcessingTools.Contracts.Services.Data.Geo.Models.IGeoSynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Geo.Data.Entity.Models.ISynonym
        where TSynonymModel : class, ProcessingTools.Contracts.Services.Data.Geo.Models.IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            return query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take);
        }
    }
}
