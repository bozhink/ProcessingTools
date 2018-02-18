// <copyright file="IMinimalTaxonNamePart.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Enumerations;

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
