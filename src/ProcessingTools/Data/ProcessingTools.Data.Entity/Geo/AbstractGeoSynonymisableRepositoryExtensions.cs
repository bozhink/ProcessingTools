﻿namespace ProcessingTools.Data.Entity.Geo
{
    using System.Linq;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    public abstract partial class AbstractGeoSynonymisableRepository<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IRepositoryAsync<TModel, TFilter>, IGeoSynonymisableRepository<TModel, TSynonymModel, TSynonymFilter>
        where TEntity : BaseModel, INameableIntegerIdentifiable, IDataModel, ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentifiable, IGeoSynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : BaseModel, INameableIntegerIdentifiable, IDataModel, ISynonym
        where TSynonymModel : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, SortOrder sortOrder)
        {
            return query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take);
        }
    }
}
