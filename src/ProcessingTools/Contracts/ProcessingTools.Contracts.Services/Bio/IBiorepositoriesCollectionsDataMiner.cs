// <copyright file="IBiorepositoriesCollectionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories collections data miner.
    /// </summary>
    public interface IBiorepositoriesCollectionsDataMiner : IDataMiner<string, ICollectionMetaModel>
    {
    }
}
