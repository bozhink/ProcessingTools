﻿// <copyright file="IMunicipalitiesFilter.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    /// <summary>
    /// Municipalities filter.
    /// </summary>
    public interface IMunicipalitiesFilter : ISynonymisableFilter
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
        /// Gets county.
        /// </summary>
        string County { get; }

        /// <summary>
        /// Gets city.
        /// </summary>
        string City { get; }
    }
}
