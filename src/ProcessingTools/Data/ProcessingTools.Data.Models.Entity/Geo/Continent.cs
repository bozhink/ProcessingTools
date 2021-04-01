// <copyright file="Continent.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Continent entity.
    /// </summary>
    public class Continent : BaseModel, ISynonymisable<ContinentSynonym>, INamedIntegerIdentified, IAbbreviatedNamed
    {
        /// <summary>
        /// Gets or sets the ID of the continent entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the continent.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated name of the continent.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets the collection of synonym entities.
        /// </summary>
        public virtual ICollection<ContinentSynonym> Synonyms { get; private set; } = new HashSet<ContinentSynonym>();

        /// <summary>
        /// Gets the collection of country entities.
        /// </summary>
        public virtual ICollection<Country> Countries { get; private set; } = new HashSet<Country>();
    }
}
