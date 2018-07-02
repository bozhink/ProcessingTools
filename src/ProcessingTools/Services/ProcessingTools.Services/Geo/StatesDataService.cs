// <copyright file="StatesDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// States data service.
    /// </summary>
    public class StatesDataService : AbstractGeoSynonymisableDataService<IStatesRepository, IState, IStatesFilter, IStateSynonym, IStateSynonymsFilter>, IStatesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IStatesRepository"/>.</param>
        public StatesDataService(IStatesRepository repository)
            : base(repository)
        {
        }
    }
}
