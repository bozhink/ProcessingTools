// <copyright file="ICountiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Counties data service.
    /// </summary>
    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, IGeoSynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
