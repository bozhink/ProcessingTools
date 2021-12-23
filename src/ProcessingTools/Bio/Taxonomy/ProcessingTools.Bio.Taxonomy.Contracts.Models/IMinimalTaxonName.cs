// <copyright file="IMinimalTaxonName.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Taxon name.
    /// </summary>
    public interface IMinimalTaxonName
    {
        /// <summary>
        /// Gets taxon name parts.
        /// </summary>
        IEnumerable<IMinimalTaxonNamePart> Parts { get; }
    }
}
