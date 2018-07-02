// <copyright file="IBlackListDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    /// <summary>
    /// Taxonomic black list data service.
    /// </summary>
    public interface IBlackListDataService : IAddableDataService<string>, IDeletableDataService<string>
    {
    }
}
