// <copyright file="IContinentsFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Filters.Geo
{
    /// <summary>
    /// Continents filter.
    /// </summary>
    public interface IContinentsFilter : ISynonymisableFilter
    {
        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }
    }
}
