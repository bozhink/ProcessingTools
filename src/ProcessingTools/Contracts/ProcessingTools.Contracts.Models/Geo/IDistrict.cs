﻿// <copyright file="IDistrict.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// District.
    /// </summary>
    public interface IDistrict : IGeoSynonymisable<IDistrictSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets country ID.
        /// </summary>
        int CountryId { get; }

        /// <summary>
        /// Gets state ID.
        /// </summary>
        int? StateId { get; }

        /// <summary>
        /// Gets province ID.
        /// </summary>
        int? ProvinceId { get; }

        /// <summary>
        /// Gets region ID.
        /// </summary>
        int? RegionId { get; }

        /// <summary>
        /// Gets municipalities.
        /// </summary>
        ICollection<IMunicipality> Municipalities { get; }

        /// <summary>
        /// Gets counties.
        /// </summary>
        ICollection<ICounty> Counties { get; }

        /// <summary>
        /// Gets cities.
        /// </summary>
        ICollection<ICity> Cities { get; }
    }
}
