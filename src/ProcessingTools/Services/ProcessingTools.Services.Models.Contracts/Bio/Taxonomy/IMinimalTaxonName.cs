// <copyright file="IMinimalTaxonName.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace ProcessingTools.Contracts.Services.Models.Bio.Taxonomy
{
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
