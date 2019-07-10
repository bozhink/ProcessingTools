// <copyright file="IContinentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models.Geo;

namespace ProcessingTools.Contracts.Services.Geo
{
    /// <summary>
    /// Continents data service.
    /// </summary>
    public interface IContinentsDataService : IDataServiceAsync<IContinent, IContinentsFilter>, IGeoSynonymisableDataService<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
