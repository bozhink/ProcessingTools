// <copyright file="IZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;

namespace ProcessingTools.Contracts.Services.Bio.ZooBank
{
    /// <summary>
    /// ZooBank JSON cloner.
    /// </summary>
    public interface IZooBankJsonCloner : IDocumentCloner<ZooBankRegistration>
    {
    }
}
