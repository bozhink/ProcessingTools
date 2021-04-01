// <copyright file="CountyModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// County model.
    /// </summary>
    public class CountyModel : ICounty
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the county.
        /// </summary>
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets the collection of cities.
        /// </summary>
        public ICollection<ICity> Cities { get; private set; } = new HashSet<ICity>();

        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the district.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the county.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the municipality.
        /// </summary>
        public int? MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the county.
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
        public ICollection<ICountySynonym> Synonyms { get; private set; } = new HashSet<ICountySynonym>();
    }
}
