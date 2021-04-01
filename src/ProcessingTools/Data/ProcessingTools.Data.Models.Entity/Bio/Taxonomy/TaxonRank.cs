// <copyright file="TaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank entity.
    /// </summary>
    public class TaxonRank
    {
        private string name;

        /// <summary>
        /// Gets or sets the ID of the taxon rank entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the taxon rank.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfRankName)]
        public string Name { get => this.name; set => this.name = value?.ToLowerInvariant(); }

        /// <summary>
        /// Gets the collection of taxon names.
        /// </summary>
        public virtual ICollection<TaxonName> Taxa { get; private set; } = new HashSet<TaxonName>();
    }
}
