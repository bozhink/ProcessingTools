// <copyright file="GbifApiV09DataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.Gbif
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Contracts;

    /// <summary>
    /// GBIF API v0.9 data requester.
    /// </summary>
    public class GbifApiV09DataRequester : IGbifApiV09DataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";
        private readonly Uri baseUri = new Uri(BaseAddress);
        private readonly IHttpRequester httpRequester;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifApiV09DataRequester"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public GbifApiV09DataRequester(IHttpRequester httpRequester)
        {
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
        }

        /// <inheritdoc/>
        public async Task<GbifApiV09ResponseModel> RequestDataAsync(string content)
        {
            string relativeUri = $"v0.9/species/match?verbose=true&name={content}";

            Uri requestUri = new Uri(this.baseUri, relativeUri);

            var result = await this.httpRequester.GetJsonToObjectAsync<GbifApiV09ResponseModel>(requestUri).ConfigureAwait(false);
            return result;
        }
    }
}
