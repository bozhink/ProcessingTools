// <copyright file="AbstractGeoSynonymisableRepositoryExtensions.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
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
        where TEntity : BaseModel, INamedIntegerIdentified, ISynonymisable<TSynonymEntity>
        where TModel : class, IIntegerIdentified, IGeoSynonymisable<TSynonymModel>
        where TFilter : IFilter
        where TSynonymEntity : BaseModel, INamedIntegerIdentified, ISynonym
        where TSynonymModel : class, IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        private IQueryable<TEntity> SelectQuery(IQueryable<TEntity> query, int skip, int take, string sortColumn, ProcessingTools.Common.Enumerations.SortOrder sortOrder)
        {
            return query.OrderByName(sortColumn, (ProcessingTools.Extensions.Linq.SortOrder)sortOrder).Skip(skip).Take(take);
        }
    }
}
