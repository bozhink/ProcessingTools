// <copyright file="GeoEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Services.Abstractions;

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
