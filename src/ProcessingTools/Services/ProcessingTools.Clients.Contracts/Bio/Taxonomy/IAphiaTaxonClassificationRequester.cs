// <copyright file="IAphiaTaxonClassificationRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

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
