// <copyright file="ProductsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Products
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Contracts.Services.Products;
    using ProcessingTools.Contracts.Services.Resources;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Products data miner.
    /// </summary>
    public class ProductsDataMiner : SimpleServiceStringDataMiner<IProductsDataService, IProduct, IFilter>, IProductsDataMiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IProductsDataService"/> instance.</param>
        public ProductsDataMiner(IProductsDataService service)
            : base(service)
        {
        }
    }
}
