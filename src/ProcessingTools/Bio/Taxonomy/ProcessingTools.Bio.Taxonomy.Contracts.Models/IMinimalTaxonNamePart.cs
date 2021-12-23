// <copyright file="IMinimalTaxonNamePart.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    using ProcessingTools.Bio.Taxonomy.Common;

    /// <summary>
    /// Taxon name part.
    /// </summary>
    public interface IMinimalTaxonNamePart
    {
        /// <summary>
        /// Gets or sets full name.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets rank.
        /// </summary>
        SpeciesPartType Rank { get; }

        /// <summary>
        /// Gets content hash.
        /// </summary>
        int ContentHash { get; }
    }
}
