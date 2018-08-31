// <copyright file="ITextFilter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
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
