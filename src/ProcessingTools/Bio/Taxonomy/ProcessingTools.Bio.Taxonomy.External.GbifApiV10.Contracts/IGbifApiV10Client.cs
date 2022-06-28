// <copyright file="IGbifApiV10Client.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;
    using ProcessingTools.Integrations.Gbif.IntegrationModels.V10;

    /// <summary>
    /// GBIF API v1.0 client.
    /// </summary>
    public interface IGbifApiV10Client
    {
        /// <summary>
        /// Search scientific name with GBIF API v0.9.
        /// </summary>
        /// <param name="name">Scientific name of the taxon which rank is searched.</param>
        /// <param name="traceId">Trace ID of the request.</param>
        /// <returns><see cref="GbifApiV10ResponseModel"/> of the GBIF API v0.9 response.</returns>
        Task<GbifApiV10ResponseModel?> GetDataPerNameAsync(string name, string? traceId);

        /// <summary>
        /// Search scientific name with GBIF API v0.9.
        /// </summary>
        /// <param name="name">Scientific name of the taxon which rank is searched.</param>
        /// <param name="traceId">Trace ID of the request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><see cref="GbifApiV10ResponseModel"/> of the GBIF API v0.9 response.</returns>
        Task<GbifApiV10ResponseModel?> GetDataPerNameAsync(string name, string? traceId, CancellationToken cancellationToken);
    }
}
