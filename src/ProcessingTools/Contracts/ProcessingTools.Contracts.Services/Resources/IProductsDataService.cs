// <copyright file="IProductsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Resources
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Products data service.
    /// </summary>
    public interface IProductsDataService : ISelectableDataServiceAsync<IProduct, IFilter>
    {
    }
}
