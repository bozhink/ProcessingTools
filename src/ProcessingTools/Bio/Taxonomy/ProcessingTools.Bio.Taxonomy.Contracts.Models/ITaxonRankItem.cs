// <copyright file="ITaxonRankItem.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Bio.Taxonomy.Common;

    /// <summary>
    /// Taxon rank item.
    /// </summary>
    public interface ITaxonRankItem
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether entity is included in the white list.
        /// </summary>
        bool IsWhiteListed { get; }

        /// <summary>
        /// Gets ranks.
        /// </summary>
        ICollection<TaxonRankType> Ranks { get; }
    }
}
