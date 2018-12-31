﻿// <copyright file="GeoEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Geo
{
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Geo;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
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
