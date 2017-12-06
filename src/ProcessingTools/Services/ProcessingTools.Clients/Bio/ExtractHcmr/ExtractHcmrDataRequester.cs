// <copyright file="ExtractHcmrDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Clients.Bio;
    using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Request data from EXTRACT.
    /// See https://extract.hcmr.gr/
    /// </summary>
    public class ExtractHcmrDataRequester : IExtractHcmrDataRequester
    {
        private const string BaseAddress = "http://tagger.jensenlab.org/";
        private const string GetEntitiesApiUrl = "GetEntities";
        private readonly INetConnectorFactory connectorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractHcmrDataRequester"/> class.
        /// </summary>
        /// <param name="connectorFactory">Factory for base clients.</param>
        public ExtractHcmrDataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <inheritdoc/>
        public async Task<ExtractHcmrResponseModel> RequestDataAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var values = new Dictionary<string, string>
            {
                { "document", content },
                { "entity_types", "-25 -26 -27" },
                { "format", "xml" }
            };

            var connector = this.connectorFactory.Create(BaseAddress);
            var result = await connector.PostXmlObjectAsync<ExtractHcmrResponseModel>(GetEntitiesApiUrl, values, Defaults.Encoding).ConfigureAwait(false);
            return result;
        }
    }
}
