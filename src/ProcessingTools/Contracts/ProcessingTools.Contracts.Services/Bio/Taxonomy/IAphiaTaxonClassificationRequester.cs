// <copyright file="IAphiaTaxonClassificationRequester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Aphia taxon classification requester.
    /// </summary>
    public interface IAphiaTaxonClassificationRequester
    {
        /// <summary>
        /// Resolves classification of specified scientific name.
        /// </summary>
        /// <param name="name">Scientific name.</param>
        /// <returns>Taxon classification model.</returns>
        Task<IList<ITaxonClassification>> ResolveScientificNameAsync(string name);
    }
}
