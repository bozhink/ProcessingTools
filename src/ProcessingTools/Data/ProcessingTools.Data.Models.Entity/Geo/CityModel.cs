// <copyright file="CityModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// City model.
    /// </summary>
    public class CityModel : ICity
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the city.
        /// </summary>
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public ICountry Country { get; set; }

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the county.
        /// </summary>
        public int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the district.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the city.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the municipality.
        /// </summary>
        public int? MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of post code.
        /// </summary>
        public ICollection<IPostCode> PostCodes { get; private set; } = new HashSet<IPostCode>();

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
        public ICollection<ICitySynonym> Synonyms { get; private set; } = new HashSet<ICitySynonym>();
    }
}
