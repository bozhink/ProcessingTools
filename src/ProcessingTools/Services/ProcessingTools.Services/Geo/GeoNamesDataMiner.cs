// <copyright file="GeoNamesDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Geo;
    using ProcessingTools.Services.Abstractions;

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
