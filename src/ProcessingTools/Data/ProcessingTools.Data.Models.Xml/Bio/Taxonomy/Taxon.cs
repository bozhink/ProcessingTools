// <copyright file="Taxon.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Xml.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon model.
    /// </summary>
    public class Taxon : ITaxonRankItem
    {
        /// <summary>
        /// Gets or sets the taxon name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether taxon name is part of the white-list.
        /// </summary>
        public bool IsWhiteListed { get; set; }

        /// <summary>
        /// Gets or sets taxon ranks.
        /// </summary>
        public ICollection<TaxonRankType> Ranks { get; set; } = new HashSet<TaxonRankType>();
    }
}
