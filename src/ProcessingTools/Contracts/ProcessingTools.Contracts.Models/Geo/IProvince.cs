// <copyright file="IProvince.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Province.
    /// </summary>
    public interface IProvince : IGeoSynonymisable<IProvinceSynonym>, INamedIntegerIdentified, IAbbreviatedNamed, IServiceModel
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
        /// Gets regions.
        /// </summary>
        ICollection<IRegion> Regions { get; }

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
