﻿// <copyright file="IGeoNamesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo names repository.
    /// </summary>
    public interface IGeoNamesRepository : IRepositoryAsync<IGeoName, ITextFilter>
    {
    }
}
