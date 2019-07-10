// <copyright file="IBiorepositoriesCollectionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Bio.Biorepositories;

namespace ProcessingTools.Contracts.Services.Bio
{
    /// <summary>
    /// Biorepositories collections data miner.
    /// </summary>
    public interface IBiorepositoriesCollectionsDataMiner : IDataMiner<string, ICollection>
    {
    }
}
