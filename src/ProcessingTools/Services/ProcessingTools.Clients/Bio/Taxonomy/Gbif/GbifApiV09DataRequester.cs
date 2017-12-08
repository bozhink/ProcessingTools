// <copyright file="GbifApiV09DataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.Gbif
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Clients.Bio.Taxonomy;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public class GbifApiV09DataRequester : IGbifApiV09DataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        private readonly INetConnectorFactory connectorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifApiV09DataRequester"/> class.
        /// </summary>
        /// <param name="connectorFactory">Net connector factory.</param>
        public GbifApiV09DataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <inheritdoc/>
        public async Task<GbifApiV09ResponseModel> RequestDataAsync(string content)
        {
            string url = $"v0.9/species/match?verbose=true&name={content}";
            var connector = this.connectorFactory.Create(BaseAddress);
            var result = await connector.GetJsonObjectAsync<GbifApiV09ResponseModel>(url).ConfigureAwait(false);
            return result;
        }
    }
}
