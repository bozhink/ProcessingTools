﻿// <copyright file="ITaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver.
    /// </summary>
    public interface ITaxonClassificationResolver : ITaxonInformationResolver<ITaxonClassification>
    {
    }
}
