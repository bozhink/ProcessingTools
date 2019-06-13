﻿namespace ProcessingTools.Data.Entity.Geo
{
    using System.Linq;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Data.Models.Entity.Geo;
    using ProcessingTools.Extensions.Linq;

    public abstract partial class AbstractGeoSynonymisableRepository<TEntity, TModel, TFilter, TSynonymEntity, TSynonymModel, TSynonymFilter> : IRepositoryAsync<TModel, TFilter>, IGeoSynonymisableRepository<TModel, TSynonymModel, TSynonymFilter>
        where TEntity : BaseModel, INamedIntegerIdentified, IDataModel, ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentified, IGeoSynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : BaseModel, INamedIntegerIdentified, IDataModel, ISynonym
        where TSynonymModel : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, SortOrder sortOrder)
        {
            return query.OrderByName(sortColumn, sortOrder).Skip(skip).Take(take);
        }
    }
}