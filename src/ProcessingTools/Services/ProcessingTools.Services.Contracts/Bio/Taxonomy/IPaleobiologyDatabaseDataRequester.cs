// <copyright file="IPaleobiologyDatabaseDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Paleobiology Database data requester.
    /// </summary>
    public interface IPaleobiologyDatabaseDataRequester : IDataRequester<PbdbAllParents>
    {
    }
}
