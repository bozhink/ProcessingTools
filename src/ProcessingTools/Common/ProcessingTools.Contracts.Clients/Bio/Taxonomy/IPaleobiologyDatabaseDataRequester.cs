// <copyright file="IPaleobiologyDatabaseDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Clients.Bio.Taxonomy
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Paleobiology Database data requester.
    /// </summary>
    public interface IPaleobiologyDatabaseDataRequester : IDataRequester<PbdbAllParents>
    {
    }
}
