// <copyright file="IEnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Environments
{
    using ProcessingTools.Services.Models.Contracts.Bio.Environments;

    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public interface IEnvoTermsDataMiner : IDataMiner<string, IEnvoTerm>
    {
    }
}
