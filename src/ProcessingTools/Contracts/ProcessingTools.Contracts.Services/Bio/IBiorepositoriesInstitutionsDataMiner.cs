// <copyright file="IBiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Bio.Biorepositories;

namespace ProcessingTools.Contracts.Services.Bio
{
    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitution>
    {
    }
}
