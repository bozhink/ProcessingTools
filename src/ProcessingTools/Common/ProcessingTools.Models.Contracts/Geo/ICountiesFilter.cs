// <copyright file="ICountiesFilter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    /// <summary>
    /// Counties filter.
    /// </summary>
    public interface ICountiesFilter : ISynonymisableFilter
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
        /// Gets province.
        /// </summary>
        string Province { get; }

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
        /// Gets city.
        /// </summary>
        string City { get; }
    }
}
