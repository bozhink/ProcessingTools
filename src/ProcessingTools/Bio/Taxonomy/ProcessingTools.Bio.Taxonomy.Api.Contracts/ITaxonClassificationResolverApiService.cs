// <copyright file="ITaxonClassificationResolverApiService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Api.Models;

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
