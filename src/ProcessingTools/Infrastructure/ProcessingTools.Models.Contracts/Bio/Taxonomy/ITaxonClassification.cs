// <copyright file="ITaxonClassification.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;

    /// <summary>
    /// Taxon classification model.
    /// </summary>
    public interface ITaxonClassification : IExtendedTaxonRank
    {
        /// <summary>
        /// Gets the classification tree of the taxon.
        /// </summary>
        ICollection<ITaxonRank> Classification { get; }
    }
}
