// <copyright file="Region.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Region entity.
    /// </summary>
    public class Region : BaseModel, ISynonymisable<RegionSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the region entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the  abbreviated name of the region.
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
        /// Gets the collection of district entities.
        /// </summary>
        public virtual ICollection<District> Districts { get; private set; } = new HashSet<District>();

        /// <summary>
        /// Gets the collection of municipality entities.
        /// </summary>
        public virtual ICollection<Municipality> Municipalities { get; private set; } = new HashSet<Municipality>();

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
        public virtual ICollection<RegionSynonym> Synonyms { get; private set; } = new HashSet<RegionSynonym>();
    }
}
