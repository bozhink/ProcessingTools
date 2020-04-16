﻿// <copyright file="IZooBankJsonCloner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.ZooBank
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.ZooBank.Json;

    /// <summary>
    /// ZooBank JSON cloner.
    /// </summary>
    public interface IZooBankJsonCloner : IDocumentCloner<ZooBankRegistration>
    {
    }
}