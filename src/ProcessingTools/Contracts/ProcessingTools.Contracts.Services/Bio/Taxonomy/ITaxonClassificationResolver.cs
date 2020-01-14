// <copyright file="ITaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver.
    /// </summary>
    public interface ITaxonClassificationResolver : ITaxonInformationResolver<ITaxonClassificationSearchResult>
    {
    }
}
