// <copyright file="Municipality.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Municipality entity.
    /// </summary>
    public class Municipality : BaseModel, ISynonymisable<MunicipalitySynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the municipality entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the municipality.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the municipality.
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
        /// Gets the collection of county entities.
        /// </summary>
        public virtual ICollection<County> Counties { get; private set; } = new HashSet<County>();

        /// <summary>
        /// Gets the collection of city entities.
        /// </summary>
        public virtual ICollection<City> Cities { get; private set; } = new HashSet<City>();

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<MunicipalitySynonym> Synonyms { get; private set; } = new HashSet<MunicipalitySynonym>();
    }
}
