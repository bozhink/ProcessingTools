// <copyright file="IZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Bio.ZooBank
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;

    /// <summary>
    /// ZooBank JSON cloner.
    /// </summary>
    public interface IZooBankJsonCloner : IDocumentCloner<ZooBankRegistration>
    {
    }
}
