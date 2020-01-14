// <copyright file="MorphologicalEpithetsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Bio;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Services.Abstractions;

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
