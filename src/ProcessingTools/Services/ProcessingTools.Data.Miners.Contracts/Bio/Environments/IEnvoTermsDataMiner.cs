// <copyright file="IEnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Bio.Environments
{
    using ProcessingTools.Services.Models.Contracts.Bio.Environments;

    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public interface IEnvoTermsDataMiner : IDataMiner<string, IEnvoTerm>
    {
    }
}
