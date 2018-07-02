// <copyright file="IExtractHcmrDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Bio.Environments
{
    using ProcessingTools.Data.Miners.Models.Contracts.Bio.Environments;

    /// <summary>
    /// EXTRACT HCMR data miner.
    /// </summary>
    public interface IExtractHcmrDataMiner : IDataMiner<string, IExtractHcmrEnvoTerm>
    {
    }
}
