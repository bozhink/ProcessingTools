// <copyright file="IMinimalTaxonNamePart.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Common.Enumerations;

namespace ProcessingTools.Contracts.Services.Models.Bio.Taxonomy
{
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
