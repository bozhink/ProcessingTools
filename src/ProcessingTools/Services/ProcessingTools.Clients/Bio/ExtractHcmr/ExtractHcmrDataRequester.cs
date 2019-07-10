// <copyright file="ExtractHcmrDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Bio;

namespace ProcessingTools.Clients.Bio.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Request data from EXTRACT.
    /// </summary>
    // See https://extract.hcmr.gr/
    public class ExtractHcmrDataRequester : IExtractHcmrDataRequester
    {
        private const string BaseAddress = "http://tagger.jensenlab.org/";
        private const string GetEntitiesApiUrl = "GetEntities";
        private readonly IHttpRequester httpRequester;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractHcmrDataRequester"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public ExtractHcmrDataRequester(IHttpRequester httpRequester)
        {
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
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
                { "format", "xml" },
            };

            Uri requestUri = UriExtensions.Append(BaseAddress, GetEntitiesApiUrl);

            var result = await this.httpRequester.PostToXmlToObjectAsync<ExtractHcmrResponseModel>(requestUri, values, Defaults.Encoding).ConfigureAwait(false);
            return result;
        }
    }
}
