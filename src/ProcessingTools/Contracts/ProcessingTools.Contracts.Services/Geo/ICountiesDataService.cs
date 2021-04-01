﻿// <copyright file="ICountiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Counties data service.
    /// </summary>
    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, IGeoSynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
