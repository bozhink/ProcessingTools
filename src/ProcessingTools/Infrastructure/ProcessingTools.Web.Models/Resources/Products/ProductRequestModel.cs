// <copyright file="ProductRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Resources.Products
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.DataResources;
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Represents request model for the products API.
    /// </summary>
    public class ProductRequestModel : IProduct
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the product object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the product object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.ProductNameMaximalLength)]
        public string Name { get; set; }
    }
}
