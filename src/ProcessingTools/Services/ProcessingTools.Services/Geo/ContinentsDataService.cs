// <copyright file="ContinentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Continents data service.
    /// </summary>
    public class ContinentsDataService : AbstractGeoSynonymisableDataService<IContinentsRepository, IContinent, IContinentsFilter, IContinentSynonym, IContinentSynonymsFilter>, IContinentsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContinentsDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IContinentsRepository"/>.</param>
        public ContinentsDataService(IContinentsRepository repository)
            : base(repository)
        {
        }
    }
}
