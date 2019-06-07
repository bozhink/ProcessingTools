// <copyright file="IStatesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// States data service.
    /// </summary>
    public interface IStatesDataService : IDataServiceAsync<IState, IStatesFilter>, IGeoSynonymisableDataService<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
