// <copyright file="ModelWithAttributeIdProperty.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Models
{
    /// <summary>
    /// Model with attribute ID property.
    /// </summary>
    internal class ModelWithAttributeIdProperty
    {
        /// <summary>
        /// Gets or sets the index value.
        /// </summary>
        [CustomId]
        public int IndexProperty { get; set; }
    }
}
