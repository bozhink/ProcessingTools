namespace ProcessingTools.Geo.Data.Entity.Abstractions.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    public abstract partial class AbstractGeoSynonymisableRepository<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IRepositoryAsync<TModel, TFilter>, IGeoSynonymisableRepository<TModel, TSynonymModel, TSynonymFilter>
        where TEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentifiable, IGeoSynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : SystemInformation, INameableIntegerIdentifiable, IDataModel, ISynonym
        where TSynonymModel : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending)
        {
            return query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take);
        }
    }
}
