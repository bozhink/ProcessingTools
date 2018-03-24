// <copyright file="IBiorepositoriesCollectionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Bio
{
    using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories collections data miner.
    /// </summary>
    public interface IBiorepositoriesCollectionsDataMiner : IDataMiner<string, ICollection>
    {
    }
}
