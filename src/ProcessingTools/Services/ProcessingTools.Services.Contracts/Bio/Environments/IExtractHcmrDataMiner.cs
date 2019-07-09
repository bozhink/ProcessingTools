// <copyright file="IExtractHcmrDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Environments
{
    using ProcessingTools.Services.Models.Contracts.Bio.Environments;

    /// <summary>
    /// EXTRACT HCMR data miner.
    /// </summary>
    public interface IExtractHcmrDataMiner : IDataMiner<string, IExtractHcmrEnvoTerm>
    {
    }
}
