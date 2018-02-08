// <copyright file="IBiorepositoriesCollectionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories collections data miner.
    /// </summary>
    public interface IBiorepositoriesCollectionsDataMiner : IDataMiner<string, ICollection>
    {
    }
}
