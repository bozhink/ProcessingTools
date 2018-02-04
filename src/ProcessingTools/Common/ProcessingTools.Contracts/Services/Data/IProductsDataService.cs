// <copyright file="IProductsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Products data service.
    /// </summary>
    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
