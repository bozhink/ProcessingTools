// <copyright file="IAphiaTaxonClassificationRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Models.Bio.Taxonomy;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Aphia taxon classification requester.
    /// </summary>
    public interface IAphiaTaxonClassificationRequester
    {
        /// <summary>
        /// Resolves classification of specified scientific name.
        /// </summary>
        /// <param name="scientificName">Scientific name.</param>
        /// <returns>Taxon classification model.</returns>
        Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName);
    }
}
