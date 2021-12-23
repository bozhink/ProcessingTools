// <copyright file="MunicipalityModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Municipality model.
    /// </summary>
    public class MunicipalityModel : IMunicipality
    {
        /// <summary>
        /// Gets or sets the abbreviated name of the municipality.
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
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the municipality.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the municipality.
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
        public ICollection<IMunicipalitySynonym> Synonyms { get; private set; } = new HashSet<IMunicipalitySynonym>();
    }
}
