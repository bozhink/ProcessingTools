// <copyright file="ITaxonClassificationResolverApiService{TResolver}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts;

    /// <summary>
    /// Generic taxon classification resolver API service.
    /// </summary>
    /// <typeparam name="TResolver">Type of the classification resolver.</typeparam>
    public interface ITaxonClassificationResolverApiService<TResolver> : ITaxonClassificationResolverApiService
        where TResolver : class, ITaxonClassificationResolver
    {
    }
}
