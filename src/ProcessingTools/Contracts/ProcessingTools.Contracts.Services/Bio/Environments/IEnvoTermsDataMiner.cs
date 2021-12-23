// <copyright file="IEnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Environments
{
    using ProcessingTools.Contracts.Models.Bio.Environments;

    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public interface IEnvoTermsDataMiner : IDataMiner<string, IEnvoTerm>
    {
    }
}
