// <copyright file="CitiesDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Geo;

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;

    /// <summary>
    /// Cities data service.
    /// </summary>
    public class CitiesDataService : AbstractGeoSynonymisableDataService<ICitiesRepository, ICity, ICitiesFilter, ICitySynonym, ICitySynonymsFilter>, ICitiesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="ICitiesRepository"/>.</param>
        public CitiesDataService(ICitiesRepository repository)
            : base(repository)
        {
        }
    }
}
