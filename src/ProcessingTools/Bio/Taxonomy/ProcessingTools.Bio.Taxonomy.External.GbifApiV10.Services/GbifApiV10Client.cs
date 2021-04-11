// <copyright file="GbifApiV10Client.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Contracts;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV10.Models;

    /// <summary>
    /// GBIF API v1.0 data requester.
    /// </summary>
    public class GbifApiV10Client : IGbifApiV10Client
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifApiV10Client"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Instance of <see cref="IHttpClientFactory"/>.</param>
        /// <param name="logger">Logger.</param>
        public GbifApiV10Client(IHttpClientFactory httpClientFactory, ILogger<GbifApiV10Client> logger)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<GbifApiV10ResponseModel?> GetDataPerNameAsync(string name, string? traceId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult(default(GbifApiV10ResponseModel?));
            }

            return this.GetDataPerNameInternalAsync(name, traceId, CancellationToken.None);
        }

        /// <inheritdoc/>
        public Task<GbifApiV10ResponseModel?> GetDataPerNameAsync(string name, string? traceId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult(default(GbifApiV10ResponseModel?));
            }

            return this.GetDataPerNameInternalAsync(name, traceId, cancellationToken);
        }

        private async Task<GbifApiV10ResponseModel?> GetDataPerNameInternalAsync(string name, string? traceId, CancellationToken cancellationToken)
        {
            var client = this.httpClientFactory.CreateClient(nameof(GbifApiV10Client));
            if (client.BaseAddress is null)
            {
                this.logger.LogWarning("GBIF API v1.0 base address is not configured. traceId={0}", traceId);
                return null;
            }

            try
            {
                IEnumerable<KeyValuePair<string?, string?>> queryParameters = new[]
                {
                    new KeyValuePair<string?, string?>("name", name),
                    new KeyValuePair<string?, string?>("verbose", "true"),
                };

                using var queryParametersContent = new FormUrlEncodedContent(queryParameters);

                string queryString = await queryParametersContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                string relativeUri = $"/v1/species/match?{queryString}";

                Uri requestUri = new Uri(client.BaseAddress, relativeUri);

                using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                requestMessage.Headers.Add("X-PT-TRACE-ID", traceId);

                using HttpResponseMessage responseMessage = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                string responseContent = await responseMessage.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    return null;
                }

                var result = JsonSerializer.Deserialize<GbifApiV10ResponseModel>(responseContent);

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GBIF API v1.0 error. traceId={0}", traceId);
                return null;
            }
        }
    }
    }
