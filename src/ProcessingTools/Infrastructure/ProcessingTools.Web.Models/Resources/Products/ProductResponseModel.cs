// <copyright file="ProductResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Resources.Products
{
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Represents response model for the products API.
    /// </summary>
    public class ProductResponseModel : IProduct
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the product object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the product object.
        /// </summary>
        public string Name { get; set; }
    }
}
