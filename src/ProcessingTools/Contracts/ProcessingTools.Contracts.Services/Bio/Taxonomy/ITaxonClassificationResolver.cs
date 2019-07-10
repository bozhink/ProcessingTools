// <copyright file="ITaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Bio.Taxonomy;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Taxon classification resolver.
    /// </summary>
    public interface ITaxonClassificationResolver : ITaxonInformationResolver<ITaxonClassification>
    {
    }
}
