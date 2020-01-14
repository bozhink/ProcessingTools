// <copyright file="IBiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitutionMetaModel>
    {
    }
}
