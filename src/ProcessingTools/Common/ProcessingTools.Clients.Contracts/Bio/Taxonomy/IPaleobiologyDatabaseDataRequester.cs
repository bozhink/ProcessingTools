// <copyright file="IPaleobiologyDatabaseDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.Models;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Paleobiology Database data requester.
    /// </summary>
    public interface IPaleobiologyDatabaseDataRequester : IDataRequester<PbdbAllParents>
    {
    }
}
