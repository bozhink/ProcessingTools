// <copyright file="IZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.ZooBank
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;

    /// <summary>
    /// ZooBank JSON cloner.
    /// </summary>
    public interface IZooBankJsonCloner : IDocumentCloner<ZooBankRegistration>
    {
    }
}
