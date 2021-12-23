// <copyright file="IPaleobiologyDatabaseDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json;

    /// <summary>
    /// Paleobiology Database data requester.
    /// </summary>
    public interface IPaleobiologyDatabaseDataRequester : IDataRequester<PbdbAllParents>
    {
    }
}
