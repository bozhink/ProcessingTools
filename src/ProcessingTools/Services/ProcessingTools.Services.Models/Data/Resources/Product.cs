﻿// <copyright file="Product.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Resources
{
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Product.
    /// </summary>
    public class Product : IProduct
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }
    }
}