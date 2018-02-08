// <copyright file="IExtractHcmrDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments;

    /// <summary>
    /// EXTRACT HCMR data miner.
    /// </summary>
    public interface IExtractHcmrDataMiner : IDataMiner<string, IExtractHcmrEnvoTerm>
    {
    }
}
