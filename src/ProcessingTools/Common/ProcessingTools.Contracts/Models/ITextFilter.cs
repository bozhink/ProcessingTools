// <copyright file="ITextFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Filter with simple text field.
    /// </summary>
    public interface ITextFilter : IFilter
    {
        /// <summary>
        /// Gets or sets the text-to-search.
        /// </summary>
        string Text { get; set; }
    }
}
