// <copyright file="IState.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// State.
    /// </summary>
    public interface IState : IGeoSynonymisable<IStateSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        /// <summary>
        /// Gets country ID.
        /// </summary>
        int CountryId { get; }

        /// <summary>
        /// Gets provinces.
        /// </summary>
        ICollection<IProvince> Provinces { get; }

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
