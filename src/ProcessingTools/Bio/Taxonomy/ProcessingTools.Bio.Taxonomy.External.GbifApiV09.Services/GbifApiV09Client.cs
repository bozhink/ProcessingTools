﻿// <copyright file="GbifApiV09Client.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Contracts;
    using ProcessingTools.Integrations.Gbif.IntegrationModels.V09;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public class GbifApiV09Client : IGbifApiV09Client
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifApiV09Client"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Instance of <see cref="IHttpClientFactory"/>.</param>
        /// <param name="logger">Logger.</param>
        public GbifApiV09Client(IHttpClientFactory httpClientFactory, ILogger<GbifApiV09Client> logger)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task<GbifApiV09ResponseModel?> GetDataPerNameAsync(string name, string? traceId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Task.FromResult(default(GbifApiV09ResponseModel?));
            }

            return this.GetDataPerNameInternalAsync(name, traceId, cancellationToken);
        }

        private async Task<GbifApiV09ResponseModel?> GetDataPerNameInternalAsync(string name, string? traceId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            var client = this.httpClientFactory.CreateClient(nameof(GbifApiV09Client));
            if (client.BaseAddress is null)
            {
                this.logger.LogWarning("GBIF API v0.9 base address is not configured. traceId={0}", traceId);
                return null;
            }

            try
            {
                IEnumerable<KeyValuePair<string, string>> queryParameters = new Dictionary<string, string>
                {
                    { "name", name },
                    { "verbose", "true" },
                };

                using var queryParametersContent = new FormUrlEncodedContent(queryParameters);

                string queryString = await queryParametersContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                Uri requestUri = new Uri($"/v0.9/species/match?{queryString}");

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

                var result = JsonSerializer.Deserialize<GbifApiV09ResponseModel>(responseContent, this.serializerOptions);

                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, "GBIF API v0.9 error. traceId={0}", traceId);
                return null;
            }
        }
    }
}
