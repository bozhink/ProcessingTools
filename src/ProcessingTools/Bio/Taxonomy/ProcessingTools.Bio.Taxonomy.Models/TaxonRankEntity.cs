// <copyright file="TaxonRankEntity.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon rank entity.
    /// </summary>
    public class TaxonRankEntity : ITaxonRankItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankEntity"/> class.
        /// </summary>
        public TaxonRankEntity()
        {
            this.Ranks = new HashSet<TaxonRankType>();
        }

        /// <inheritdoc/>
        public bool IsWhiteListed { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public ICollection<TaxonRankType> Ranks { get; }
    }
}
