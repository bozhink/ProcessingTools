// <copyright file="County.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// County entity.
    /// </summary>
    public class County : BaseModel, ISynonymisable<CountySynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the county entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the county.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the county.
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
        /// Gets the collection of city entities.
        /// </summary>
        public virtual ICollection<City> Cities { get; private set; } = new HashSet<City>();

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<CountySynonym> Synonyms { get; private set; } = new HashSet<CountySynonym>();
    }
}
