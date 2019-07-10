// <copyright file="ICountiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Geo;

namespace ProcessingTools.Contracts.Services.Geo
{
    /// <summary>
    /// Counties data service.
    /// </summary>
    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, IGeoSynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
