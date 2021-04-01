// <copyright file="ITaxonClassificationResolverApiService{TResolver}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Generic taxon classification resolver API service.
    /// </summary>
    /// <typeparam name="TResolver">Type of the classification resolver.</typeparam>
    public interface ITaxonClassificationResolverApiService<TResolver> : ITaxonClassificationResolverApiService
        where TResolver : class, ITaxonClassificationResolver
    {
    }
}
