// <copyright file="ISynonymisableFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Filters.Geo
{
    /// <summary>
    /// Base filter for synonyms.
    /// </summary>
    public interface ISynonymisableFilter : IGeoFilter
    {
        /// <summary>
        /// Gets synonym.
        /// </summary>
        string Synonym { get; }
    }
}
