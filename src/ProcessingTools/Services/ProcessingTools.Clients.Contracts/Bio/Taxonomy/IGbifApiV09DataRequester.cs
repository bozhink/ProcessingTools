// <copyright file="IGbifApiV09DataRequester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Contracts;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public interface IGbifApiV09DataRequester : IDataRequester<GbifApiV09ResponseModel>
    {
    }
}
