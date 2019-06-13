﻿// <copyright file="ICountry.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Country.
    /// </summary>
    public interface ICountry : IGeoSynonymisable<ICountrySynonym>, INamedIntegerIdentified, IAbbreviatedNamed, IServiceModel
    {
        /// <summary>
        /// Gets calling code.
        /// </summary>
        string CallingCode { get; }

        /// <summary>
        /// Gets language code.
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        /// Gets ISO 639 code.
        /// </summary>
        string Iso639xCode { get; }

        /// <summary>
        /// Gets continents.
        /// </summary>
        ICollection<IContinent> Continents { get; }

        /// <summary>
        /// Gets states.
        /// </summary>
        ICollection<IState> States { get; }

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
