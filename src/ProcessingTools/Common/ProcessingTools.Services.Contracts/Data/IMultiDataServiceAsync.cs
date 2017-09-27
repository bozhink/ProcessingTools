// <copyright file="IMultiDataServiceAsync.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts;

    public interface IMultiDataServiceAsync<TModel, TFilter> : ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<object> DeleteAsync(params TModel[] models);

        Task<object> DeleteAsync(params object[] ids);

        Task<object> InsertAsync(params TModel[] models);

        Task<object> UpdateAsync(params TModel[] models);
    }
}
