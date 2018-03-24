// <copyright file="GeoNamesDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Geo
{
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Geo;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
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
