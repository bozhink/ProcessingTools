// <copyright file="MorphologicalEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Bio
{
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Bio;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Morphological epithets data miner.
    /// </summary>
    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, IMorphologicalEpithet, IFilter>, IMorphologicalEpithetsDataMiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MorphologicalEpithetsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IMorphologicalEpithetsDataService"/> instance.</param>
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
