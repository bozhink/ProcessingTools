// <copyright file="RegionModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Region model.
    /// </summary>
    public class RegionModel : IRegion
    {
        /// <summary>
        /// Gets or sets the  abbreviated name of the region.
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
        /// Gets or sets the ID of the region.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the collection of municipalities.
        /// </summary>
        public ICollection<IMunicipality> Municipalities { get; private set; } = new HashSet<IMunicipality>();

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the province.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the state.
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        public ICollection<IRegionSynonym> Synonyms { get; private set; } = new HashSet<IRegionSynonym>();
    }
}
