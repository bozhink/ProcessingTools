// <copyright file="IAboveGenusTaxaRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Data;

    /// <summary>
    /// Taxonomic black list data service.
    /// </summary>
    public interface IBlackListDataService : IAddableDataService<string>, IDeletableDataService<string>
    {
    }
}
