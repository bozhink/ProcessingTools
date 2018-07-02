// <copyright file="IStatesRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// States repository.
    /// </summary>
    public interface IStatesRepository : IRepositoryAsync<IState, IStatesFilter>, IGeoSynonymisableRepository<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
