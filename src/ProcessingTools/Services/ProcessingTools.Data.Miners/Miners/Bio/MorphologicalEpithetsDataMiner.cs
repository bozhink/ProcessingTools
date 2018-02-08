// <copyright file="MorphologicalEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Services.Data.Bio;
    using ProcessingTools.Services.Contracts.Bio;

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
