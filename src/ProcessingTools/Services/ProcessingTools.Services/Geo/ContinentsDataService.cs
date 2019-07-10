// <copyright file="ContinentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Geo;

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

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
