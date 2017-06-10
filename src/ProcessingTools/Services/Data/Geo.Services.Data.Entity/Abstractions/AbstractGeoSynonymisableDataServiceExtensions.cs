namespace ProcessingTools.Geo.Services.Data.Entity.Abstractions
{
    using System.Linq;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Contracts.Services.Data.Geo.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Models;

    public abstract partial class AbstractGeoSynonymisableDataService<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IDataServiceAsync<TModel, TFilter>, ISynonymisableDataService<TModel, TSynonymModel, TSynonymFilter>
        where TEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Contracts.Data.Geo.Models.ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentifiable, ISynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ProcessingTools.Contracts.Data.Geo.Models.ISynonym
        where TSynonymModel : class, ISynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            return query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take);
        }
    }
}
