// <copyright file="IGenericDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public interface IGenericDataService<TModel>
        where TModel : IServiceModel
    {
        Task<object> AddAsync(object userId, TModel model);

        Task<object> UpdateAsync(object userId, TModel model);

        Task<object> DeleteAsync(object userId, object id);

        Task<TModel> GetAsync(object userId, object id);

        Task<IEnumerable<TModel>> SelectAsync(object userId, int skip, int take, Expression<Func<TModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TModel, bool>> filter = null);
    }
}
