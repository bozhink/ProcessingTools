// <copyright file="GeoEpithetsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Geo epithets data service.
    /// </summary>
    public class GeoEpithetsDataService : AbstractGeoMultiDataService<IGeoEpithetsRepository, IGeoEpithet, ITextFilter>, IGeoEpithetsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoEpithetsDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IGeoEpithetsRepository"/>.</param>
        public GeoEpithetsDataService(IGeoEpithetsRepository repository)
            : base(repository)
        {
        }
    }
}
