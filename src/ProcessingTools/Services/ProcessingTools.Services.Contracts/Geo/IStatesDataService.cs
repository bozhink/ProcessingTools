// <copyright file="IStatesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// States data service.
    /// </summary>
    public interface IStatesDataService : IDataServiceAsync<IState, IStatesFilter>, IGeoSynonymisableDataService<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
