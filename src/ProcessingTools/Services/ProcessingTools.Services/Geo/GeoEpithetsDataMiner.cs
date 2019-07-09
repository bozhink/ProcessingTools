// <copyright file="GeoEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    /// <summary>
    /// Geo epithets data miner.
    /// </summary>
    public class GeoEpithetsDataMiner : SimpleServiceStringDataMiner<IGeoEpithetsDataService, IGeoEpithet, ITextFilter>, IGeoEpithetsDataMiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoEpithetsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IGeoEpithetsDataService"/> instance.</param>
        public GeoEpithetsDataMiner(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}
