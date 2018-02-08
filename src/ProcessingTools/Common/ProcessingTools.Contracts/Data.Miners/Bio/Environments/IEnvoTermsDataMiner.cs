// <copyright file="IEnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Environments;

    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public interface IEnvoTermsDataMiner : IDataMiner<string, IEnvoTerm>
    {
    }
}
