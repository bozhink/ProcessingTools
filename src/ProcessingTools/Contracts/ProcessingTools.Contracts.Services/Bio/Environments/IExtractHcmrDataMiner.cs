// <copyright file="IExtractHcmrDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    using ProcessingTools.Contracts.Models.Bio.Environments;

    /// <summary>
    /// EXTRACT HCMR data miner.
    /// </summary>
    public interface IExtractHcmrDataMiner : IDataMiner<string, IExtractHcmrEnvoTerm>
    {
    }
}
