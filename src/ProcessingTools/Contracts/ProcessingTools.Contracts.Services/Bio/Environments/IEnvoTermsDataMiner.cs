// <copyright file="IEnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Bio.Environments;

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public interface IEnvoTermsDataMiner : IDataMiner<string, IEnvoTerm>
    {
    }
}
