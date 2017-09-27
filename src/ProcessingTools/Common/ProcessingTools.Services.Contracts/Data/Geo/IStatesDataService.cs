// <copyright file="IStatesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IStatesDataService : IDataServiceAsync<IState, IStatesFilter>, IGeoSynonymisableDataService<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
