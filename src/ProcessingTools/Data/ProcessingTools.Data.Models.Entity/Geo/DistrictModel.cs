// <copyright file="DistrictModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// District model.
    /// </summary>
    public class DistrictModel : IDistrict
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the district.
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
        /// Gets or sets the ID of the district.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the collection of municipalities.
        /// </summary>
        public ICollection<IMunicipality> Municipalities { get; private set; } = new HashSet<IMunicipality>();

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the province.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the region.
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the state.
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Gets the collection of synonyms.
        /// </summary>
        public ICollection<IDistrictSynonym> Synonyms { get; private set; } = new HashSet<IDistrictSynonym>();
    }
}
