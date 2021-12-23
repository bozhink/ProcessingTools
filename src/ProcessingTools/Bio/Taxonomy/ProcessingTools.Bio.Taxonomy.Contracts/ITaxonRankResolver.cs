// <copyright file="ITaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon rank resolver.
    /// </summary>
    public interface ITaxonRankResolver : ITaxonInformationResolver<ITaxonRankSearchResult>
    {
    }
}
