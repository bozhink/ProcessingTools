// <copyright file="IProductsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Resources
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Products data service.
    /// </summary>
    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
