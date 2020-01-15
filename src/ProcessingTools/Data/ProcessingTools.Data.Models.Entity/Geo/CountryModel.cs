// <copyright file="CountryModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Country model.
    /// </summary>
    public class CountryModel : ICountry
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the country.
        /// </summary>
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the calling code of the country.
        /// </summary>
        public string CallingCode { get; set; }

        /// <summary>
        /// Gets the collection of cities.
        /// </summary>
        public ICollection<ICity> Cities { get; private set; } = new HashSet<ICity>();

        /// <summary>
        /// Gets the collection of continents.
        /// </summary>
        public ICollection<IContinent> Continents { get; private set; } = new HashSet<IContinent>();

        /// <summary>
        /// Gets the collection of counties.
        /// </summary>
        public ICollection<ICounty> Counties { get; private set; } = new HashSet<ICounty>();

        /// <summary>
        /// Gets the collection of districts.
        /// </summary>
        public ICollection<IDistrict> Districts { get; private set; } = new HashSet<IDistrict>();

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ISO639x code of the country.
        /// </summary>
        public string Iso639xCode { get; set; }

        /// <summary>
        /// Gets or sets the language code of the country.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets the collection of municipalities.
        /// </summary>
        public ICollection<IMunicipality> Municipalities { get; private set; } = new HashSet<IMunicipality>();

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of provinces.
        /// </summary>
        public ICollection<IProvince> Provinces { get; private set; } = new HashSet<IProvince>();

        /// <summary>
        /// Gets the collection of regions.
        /// </summary>
        public ICollection<IRegion> Regions { get; private set; } = new HashSet<IRegion>();

        /// <summary>
        /// Gets the collection of states.
        /// </summary>
        public ICollection<IState> States { get; private set; } = new HashSet<IState>();

        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        public ICollection<ICountrySynonym> Synonyms { get; private set; } = new HashSet<ICountrySynonym>();
    }
}
