// <copyright file="IRepositoryAsync.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public interface IRepositoryAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<object> DeleteAsync(TModel model);

        Task<object> DeleteAsync(object id);

        Task<TModel> GetByIdAsync(object id);

        Task<object> InsertAsync(TModel model);

        Task<object> SaveChangesAsync();

        Task<TModel[]> SelectAsync(TFilter filter);

        Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending);

        Task<long> SelectCountAsync(TFilter filter);

        Task<object> UpdateAsync(TModel model);
    }
}
