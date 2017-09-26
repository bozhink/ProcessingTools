// <copyright file="IPostCodesFilter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Filters.Geo
{
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Post codes filter.
    /// </summary>
    public interface IPostCodesFilter : IIdentifiable<int?>, IFilter
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
        /// Gets county.
        /// </summary>
        string County { get; }

        /// <summary>
        /// Gets city.
        /// </summary>
        string City { get; }
    }
}
