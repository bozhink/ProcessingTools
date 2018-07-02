// <copyright file="IExtendedTaxonRank.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
{
    /// <summary>
    /// Extended taxon rank model.
    /// </summary>
    public interface IExtendedTaxonRank : ITaxonRank
    {
        /// <summary>
        /// Gets the canonical name.
        /// </summary>
        string CanonicalName { get; }

        /// <summary>
        /// Gets the authority.
        /// </summary>
        string Authority { get; }
    }
}
