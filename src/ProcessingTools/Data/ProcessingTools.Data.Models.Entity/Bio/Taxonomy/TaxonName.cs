// <copyright file="TaxonName.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon name entity.
    /// </summary>
    public class TaxonName
    {
        /// <summary>
        /// Gets or sets the ID of the taxon name entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the value of the taxon name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfTaxonName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this TaxonName must appear
        /// in the white list for taxon tagging.
        /// </summary>
        public bool WhiteListed { get; set; } = false;

        /// <summary>
        /// Gets the collection of taxon rank entities.
        /// </summary>
        public virtual ICollection<TaxonRank> Ranks { get; private set; } = new HashSet<TaxonRank>();
    }
}
