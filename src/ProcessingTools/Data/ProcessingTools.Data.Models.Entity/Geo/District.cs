// <copyright file="District.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// District entity.
    /// </summary>
    public class District : BaseModel, ISynonymisable<DistrictSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the district entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the district.
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
        /// Gets the collection of the municipality entities.
        /// </summary>
        public virtual ICollection<Municipality> Municipalities { get; private set; } = new HashSet<Municipality>();

        /// <summary>
        /// Gets the collection of the county entities.
        /// </summary>
        public virtual ICollection<County> Counties { get; private set; } = new HashSet<County>();

        /// <summary>
        /// Gets the collection of city entities.
        /// </summary>
        public virtual ICollection<City> Cities { get; private set; } = new HashSet<City>();

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<DistrictSynonym> Synonyms { get; private set; } = new HashSet<DistrictSynonym>();
    }
}
