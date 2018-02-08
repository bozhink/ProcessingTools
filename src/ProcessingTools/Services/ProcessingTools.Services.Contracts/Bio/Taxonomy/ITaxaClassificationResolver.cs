// <copyright file="ITaxaClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver.
    /// </summary>
    public interface ITaxaClassificationResolver : ITaxaInformationResolver<ITaxonClassification>
    {
    }
}
