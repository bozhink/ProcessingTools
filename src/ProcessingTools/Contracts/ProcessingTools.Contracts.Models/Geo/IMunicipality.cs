// <copyright file="IMunicipality.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;

    /// <summary>
    /// Municipality.
    /// </summary>
    public interface IMunicipality : IGeoSynonymisable<IMunicipalitySynonym>, INamedIntegerIdentified, IAbbreviatedNamed
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
        /// Gets district ID.
        /// </summary>
        int? DistrictId { get; }

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
