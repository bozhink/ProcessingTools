// <copyright file="IMunicipality.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Municipality.
    /// </summary>
    public interface IMunicipality : IGeoSynonymisable<IMunicipalitySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
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
