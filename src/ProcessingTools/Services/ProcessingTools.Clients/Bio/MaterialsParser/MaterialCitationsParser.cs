// <copyright file="MaterialCitationsParser.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.MaterialsParser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Material citations parser.
    /// </summary>
    public class MaterialCitationsParser : IMaterialCitationsParser
    {
        private const string BaseAddress = "http://plazi2.cs.umb.edu";
        private const string ParserUrl = "/GgWS/wss/invokeFunction";

        private readonly INetConnectorFactory connectorFactory;
        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialCitationsParser"/> class.
        /// </summary>
        /// <param name="connectorFactory">Factory for base clients.</param>
        public MaterialCitationsParser(INetConnectorFactory connectorFactory)
            : this(connectorFactory, Defaults.Encoding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialCitationsParser"/> class.
        /// </summary>
        /// <param name="connectorFactory">Factory for base clients.</param>
        /// <param name="encoding">Text encoding.</param>
        public MaterialCitationsParser(INetConnectorFactory connectorFactory, Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
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
                { "INTERACTIVE", "no" }
            };

            var connector = this.connectorFactory.Create(BaseAddress);
            var response = await connector.PostAsync(ParserUrl, values, this.encoding).ConfigureAwait(false);

            return response;
        }
    }
}
