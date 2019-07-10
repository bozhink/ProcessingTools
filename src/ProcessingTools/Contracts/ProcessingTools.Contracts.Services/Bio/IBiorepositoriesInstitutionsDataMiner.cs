// <copyright file="IBiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Contracts.Services.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitution>
    {
    }
}
