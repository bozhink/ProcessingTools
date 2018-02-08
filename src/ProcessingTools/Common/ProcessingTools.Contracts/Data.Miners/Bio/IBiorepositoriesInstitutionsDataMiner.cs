// <copyright file="IBiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitution>
    {
    }
}
