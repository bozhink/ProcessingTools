// <copyright file="GlobalNamesResolverDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.GlobalNamesResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Global Names Resolver data requester.
    /// </summary>
    public class GlobalNamesResolverDataRequester : IGlobalNamesResolverDataRequester
    {
        private const string BaseAddress = "http://resolver.globalnames.org";
        private const string ApiUrl = "name_resolvers.xml";
        private readonly IHttpRequester httpRequester;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalNamesResolverDataRequester"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public GlobalNamesResolverDataRequester(IHttpRequester httpRequester)
        {
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId)
        {
            string searchString = this.BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
            Uri requestUri = UriExtensions.Append(BaseAddress, $"{ApiUrl}?{searchString}");

            string response = await this.httpRequester.GetStringAsync(requestUri, ContentTypes.Xml).ConfigureAwait(false);
            return response.ToXmlDocument();
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId)
        {
            string postData = this.BuildGlobalNamesResolverSearchString(scientificNames, sourceId);

            Uri requestUri = UriExtensions.Append(BaseAddress, ApiUrl);

            var response = await this.httpRequester.PostAsync(requestUri, postData, ContentTypes.UrlEncoded).ConfigureAwait(false);
            return response.ToXmlDocument();
        }

        /// <inheritdoc/>
        public async Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId)
        {
            IDictionary<string, string> values = new Dictionary<string, string>
            {
                { "data", string.Join("\r\n", scientificNames) },
            };

            if (sourceId != null)
            {
                values.Add("data_source_ids", string.Join("|", sourceId));
            }

            Uri requestUri = UriExtensions.Append(BaseAddress, ApiUrl);

            var response = await this.httpRequester.PostToStringAsync(requestUri, values, Defaults.Encoding).ConfigureAwait(false);
            return response.ToXmlDocument();
        }

        private string BuildGlobalNamesResolverSearchString(string[] scientificNames, int[] sourceId)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(scientificNames));
            }

            StringBuilder searchStringBuilder = new StringBuilder();
            searchStringBuilder.Append("names=");

            Regex whiteSpaces = new Regex(@"\s+");
            searchStringBuilder.Append(string.Join("|", scientificNames.Select(s => whiteSpaces.Replace(s.Trim(), "+"))));

            if (sourceId != null)
            {
                searchStringBuilder.Append("&data_source_ids=");
                searchStringBuilder.Append(string.Join("|", sourceId));
            }

            string searchString = searchStringBuilder.ToString();

            return searchString;
        }
    }
}
