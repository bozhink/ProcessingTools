// <copyright file="ModelWithAttributeIdProperty.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests.Models
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
