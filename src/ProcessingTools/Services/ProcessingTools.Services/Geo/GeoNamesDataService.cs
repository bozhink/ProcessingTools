// <copyright file="GeoNamesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Geo names data service.
    /// </summary>
    public class GeoNamesDataService : AbstractGeoMultiDataService<IGeoNamesRepository, IGeoName, ITextFilter>, IGeoNamesDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoNamesDataService"/> class.
        /// </summary>
        /// <param name="repository">Instance of <see cref="IGeoNamesRepository"/>.</param>
        public GeoNamesDataService(IGeoNamesRepository repository)
            : base(repository)
        {
        }
    }
}
