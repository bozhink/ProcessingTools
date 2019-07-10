// <copyright file="IMinimalTaxonName.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Bio.Taxonomy
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
