// <copyright file="ITaxaRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver.
    /// </summary>
    public interface ITaxaRankResolver : ITaxaInformationResolver<ITaxonRank>
    {
    }
}
