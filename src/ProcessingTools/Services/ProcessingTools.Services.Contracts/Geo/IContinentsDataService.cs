// <copyright file="IContinentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Continents data service.
    /// </summary>
    public interface IContinentsDataService : IDataServiceAsync<IContinent, IContinentsFilter>, IGeoSynonymisableDataService<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
