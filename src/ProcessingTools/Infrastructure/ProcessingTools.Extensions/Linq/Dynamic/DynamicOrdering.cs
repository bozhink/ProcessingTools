// <copyright file="DynamicOrdering.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    using System.Linq.Expressions;

    /// <summary>
    /// Dynamic ordering.
    /// </summary>
    internal class DynamicOrdering
    {
        /// <summary>
        /// Gets or sets the selector.
        /// </summary>
        public Expression Selector { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether order is ascending.
        /// </summary>
        public bool Ascending { get; set; }
    }
}
