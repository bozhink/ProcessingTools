// <copyright file="IStatesFilter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    /// <summary>
    /// States filter.
    /// </summary>
    public interface IStatesFilter : ISynonymisableFilter
    {
        /// <summary>
        /// Gets country.
        /// </summary>
        string Country { get; }

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
