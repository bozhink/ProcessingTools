// <copyright file="IExtractHcmrDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Bio.Environments;

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    /// <summary>
    /// EXTRACT HCMR data miner.
    /// </summary>
    public interface IExtractHcmrDataMiner : IDataMiner<string, IExtractHcmrEnvoTerm>
    {
    }
}
