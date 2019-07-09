// <copyright file="GeoNamesDataMiner.cs" company="ProcessingTools">
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
    /// Geo names data miner.
    /// </summary>
    public class GeoNamesDataMiner : SimpleServiceStringDataMiner<IGeoNamesDataService, IGeoName, ITextFilter>, IGeoNamesDataMiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoNamesDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IGeoNamesDataService"/> instance.</param>
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
