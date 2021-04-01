// <copyright file="MaterialCitationsParser.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.MaterialsParser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Material citations parser.
    /// </summary>
    public class MaterialCitationsParser : IMaterialCitationsParser
    {
        private const string BaseAddress = "http://plazi2.cs.umb.edu";
        private const string ParserUrl = "/GgWS/wss/invokeFunction";

        private readonly IHttpRequester httpRequester;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialCitationsParser"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public MaterialCitationsParser(IHttpRequester httpRequester)
            : this(httpRequester, Defaults.Encoding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialCitationsParser"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        /// <param name="encoding">Text encoding.</param>
        public MaterialCitationsParser(IHttpRequester httpRequester, Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
        }

        /// <inheritdoc/>
        public async Task<string> ParseAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var values = new Dictionary<string, string>
            {
                { "data", content },
                { "functionName", "XmlExample.webService" },
                { "dataFormat", "XML" },
                { "INTERACTIVE", "no" },
            };

            Uri requestUri = UriExtensions.Append(BaseAddress, ParserUrl);

            var response = await this.httpRequester.PostToStringAsync(requestUri, values, this.encoding).ConfigureAwait(false);

            return response;
        }
    }
}
