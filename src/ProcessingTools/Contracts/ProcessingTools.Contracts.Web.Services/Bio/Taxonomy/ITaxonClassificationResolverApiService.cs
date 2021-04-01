// <copyright file="ITaxonClassificationResolverApiService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver API service.
    /// </summary>
    public interface ITaxonClassificationResolverApiService
    {
        /// <summary>
        /// Resolves classification information about specified taxon names.
        /// </summary>
        /// <param name="taxonNames">Taxon names to be resolved.</param>
        /// <returns>Classification information about specified taxon names.</returns>
        Task<IList<TaxonClassificationResponseModel>> ResolveAsync(IEnumerable<string> taxonNames);
    }
}
