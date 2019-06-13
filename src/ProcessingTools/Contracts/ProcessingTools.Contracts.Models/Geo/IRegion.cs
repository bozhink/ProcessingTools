﻿// <copyright file="IRegion.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Region.
    /// </summary>
    public interface IRegion : IGeoSynonymisable<IRegionSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets country.
        /// </summary>
        int CountryId { get; }

        /// <summary>
        /// Gets state.
        /// </summary>
        int? StateId { get; }

        /// <summary>
        /// Gets province.
        /// </summary>
        int? ProvinceId { get; }

        /// <summary>
        /// Gets districts.
        /// </summary>
        ICollection<IDistrict> Districts { get; }

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
