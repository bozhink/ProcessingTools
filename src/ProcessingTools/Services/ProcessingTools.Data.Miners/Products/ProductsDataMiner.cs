// <copyright file="ProductsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Products
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Products;
    using ProcessingTools.Services.Contracts.Resources;

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
