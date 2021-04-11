// <copyright file="GbifApiV09Client.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.Gbif
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Extensions;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public class GbifApiV09Client : IGbifApiV09Client
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IJsonDeserializer<GbifApiV09ResponseModel> deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifApiV09Client"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Instance of <see cref="IHttpClientFactory"/>.</param>
        /// <param name="deserializer">Instance of <see cref="IJsonDeserializer{GbifApiV09ResponseModel}"/>.</param>
        public GbifApiV09Client(IHttpClientFactory httpClientFactory, IJsonDeserializer<GbifApiV09ResponseModel> deserializer)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        /// <inheritdoc/>
        public async Task<GbifApiV09ResponseModel> GetDataPerNameAsync(string name)
        {
            IDictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "name", name },
                { "verbose", "true" },
            };

            using var content = new FormUrlEncodedContent(queryParameters);

            string queryString = await content.ReadAsStringAsync().ConfigureAwait(false);

            string relativeUri = $"v0.9/species/match?{queryString}";

            var client = this.httpClientFactory.CreateClient(nameof(GbifApiV09Client));

            var response = await client.GetStringAsync(relativeUri).ConfigureAwait(false);

            var result = this.deserializer.Deserialize(response);

            return result;
        }
    }
}
