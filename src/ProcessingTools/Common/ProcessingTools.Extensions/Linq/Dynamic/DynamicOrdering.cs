// <copyright file="DynamicOrdering.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace System.Linq.Dynamic
{
    using System.Linq.Expressions;

    /// <summary>
    /// Dynamic Ordering
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
