// <copyright file="ISynonymFilter.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    /// <summary>
    /// Synonym filter.
    /// </summary>
    public interface ISynonymFilter : IGeoFilter
    {
        /// <summary>
        /// Gets language code.
        /// </summary>
        int? LanguageCode { get; }
    }
}
