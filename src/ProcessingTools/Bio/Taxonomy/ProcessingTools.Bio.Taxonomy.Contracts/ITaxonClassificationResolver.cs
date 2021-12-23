// <copyright file="ITaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon classification resolver.
    /// </summary>
    public interface ITaxonClassificationResolver : ITaxonInformationResolver<ITaxonClassificationSearchResult>
    {
    }
}
