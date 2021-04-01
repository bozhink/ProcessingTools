// <copyright file="City.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// City entity.
    /// </summary>
    public class City : BaseModel, ISynonymisable<CitySynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the city entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfCityName)]
        [MaxLength(ValidationConstants.MaximalLengthOfCityName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the city.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the country entity.
        /// </summary>
        public virtual int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country entity.
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Gets or sets the ID of the state entity.
        /// </summary>
        public virtual int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the state entity.
        /// </summary>
        public virtual State State { get; set; }

        /// <summary>
        /// Gets or sets the ID of the province entity.
        /// </summary>
        public virtual int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the province entity.
        /// </summary>
        public virtual Province Province { get; set; }

        /// <summary>
        /// Gets or sets the ID of the region entity.
        /// </summary>
        public virtual int? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the region entity.
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// Gets or sets the ID of the district entity.
        /// </summary>
        public virtual int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the district entity.
        /// </summary>
        public virtual District District { get; set; }

        /// <summary>
        /// Gets or sets the ID of the municipality entity.
        /// </summary>
        public virtual int? MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the municipality entity.
        /// </summary>
        public virtual Municipality Municipality { get; set; }

        /// <summary>
        /// Gets or sets the ID of the county entity.
        /// </summary>
        public virtual int? CountyId { get; set; }

        /// <summary>
        /// Gets or sets the county entity.
        /// </summary>
        public virtual County County { get; set; }

        /// <summary>
        /// Gets the collection of post code entities.
        /// </summary>
        public virtual ICollection<PostCode> PostCodes { get; private set; } = new HashSet<PostCode>();

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<CitySynonym> Synonyms { get; private set; } = new HashSet<CitySynonym>();
    }
}
