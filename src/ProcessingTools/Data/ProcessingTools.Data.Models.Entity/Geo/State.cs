// <copyright file="State.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// State entity.
    /// </summary>
    public class State : BaseModel, ISynonymisable<StateSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the state entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the state.
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
        /// Gets the collection of province entities.
        /// </summary>
        public virtual ICollection<Province> Provinces { get; private set; } = new HashSet<Province>();

        /// <summary>
        /// Gets the collection of region entities.
        /// </summary>
        public virtual ICollection<Region> Regions { get; private set; } = new HashSet<Region>();

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
        public virtual ICollection<StateSynonym> Synonyms { get; private set; } = new HashSet<StateSynonym>();
    }
}
