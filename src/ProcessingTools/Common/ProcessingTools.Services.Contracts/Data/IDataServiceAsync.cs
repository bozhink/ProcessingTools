// <copyright file="IDataServiceAsync.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts;

    public interface IDataServiceAsync<TModel, TFilter> : ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<object> DeleteAsync(TModel model);

        Task<object> DeleteAsync(object id);

        Task<object> InsertAsync(TModel model);

        Task<object> UpdateAsync(TModel model);
    }
}
