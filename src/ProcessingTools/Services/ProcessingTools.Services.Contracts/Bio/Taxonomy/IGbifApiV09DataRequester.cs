// <copyright file="IGbifApiV09DataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public interface IGbifApiV09DataRequester : IDataRequester<GbifApiV09ResponseModel>
    {
    }
}
