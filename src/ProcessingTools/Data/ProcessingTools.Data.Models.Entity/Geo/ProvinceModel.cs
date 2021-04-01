// <copyright file="ProvinceModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Province model.
    /// </summary>
    public class ProvinceModel : IProvince
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the province.
        /// </summary>
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets the collection of cities.
        /// </summary>
        public ICollection<ICity> Cities { get; private set; } = new HashSet<ICity>();

        /// <summary>
        /// Gets the collection of counties.
        /// </summary>
        public ICollection<ICounty> Counties { get; private set; } = new HashSet<ICounty>();

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets the collection of districts.
        /// </summary>
        public ICollection<IDistrict> Districts { get; private set; } = new HashSet<IDistrict>();

        /// <summary>
        /// Gets or sets the ID of the province.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the collection of municipalities.
        /// </summary>
        public ICollection<IMunicipality> Municipalities { get; private set; } = new HashSet<IMunicipality>();

        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of regions.
        /// </summary>
        public ICollection<IRegion> Regions { get; private set; } = new HashSet<IRegion>();

        /// <summary>
        /// Gets or sets the ID of the state.
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        public ICollection<IProvinceSynonym> Synonyms { get; private set; } = new HashSet<IProvinceSynonym>();
    }
}
