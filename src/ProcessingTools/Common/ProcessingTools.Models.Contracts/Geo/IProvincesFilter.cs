// <copyright file="IProvincesFilter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    /// <summary>
    /// Provinces filter.
    /// </summary>
    public interface IProvincesFilter : ISynonymisableFilter
    {
        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }

        /// <summary>
        /// Gets state.
        /// </summary>
        string State { get; }

        /// <summary>
        /// Gets region.
        /// </summary>
        string Region { get; }

        /// <summary>
        /// Gets district.
        /// </summary>
        string District { get; }

        /// <summary>
        /// Gets municipality.
        /// </summary>
        string Municipality { get; }

        /// <summary>
        /// Gets county.
        /// </summary>
        string County { get; }

        /// <summary>
        /// Gets city.
        /// </summary>
        string City { get; }
    }
}
