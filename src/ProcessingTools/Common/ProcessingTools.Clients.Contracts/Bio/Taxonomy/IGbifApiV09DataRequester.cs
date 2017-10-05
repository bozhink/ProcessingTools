// <copyright file="IGbifApiV09DataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models;
    using ProcessingTools.Contracts;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public interface IGbifApiV09DataRequester : IDataRequester<GbifApiV09ResponseModel>
    {
    }
}
