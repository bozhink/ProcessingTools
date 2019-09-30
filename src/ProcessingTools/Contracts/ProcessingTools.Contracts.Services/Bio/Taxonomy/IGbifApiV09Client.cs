// <copyright file="IGbifApiV09Client.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;

    /// <summary>
    /// GBIF API v0.9 client.
    /// </summary>
    public interface IGbifApiV09Client
    {
        /// <summary>
        /// Search scientific name with GBIF API v0.9.
        /// </summary>
        /// <param name="name">Scientific name of the taxon which rank is searched.</param>
        /// <returns><see cref="GbifApiV09ResponseModel"/> of the GBIF API v0.9 response.</returns>
        Task<GbifApiV09ResponseModel> GetDataPerNameAsync(string name);
    }
}
