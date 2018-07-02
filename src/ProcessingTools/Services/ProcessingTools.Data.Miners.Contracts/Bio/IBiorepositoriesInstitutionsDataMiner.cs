// <copyright file="IBiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Bio
{
    using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitution>
    {
    }
}
