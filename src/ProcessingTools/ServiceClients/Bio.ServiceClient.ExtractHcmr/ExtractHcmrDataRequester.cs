namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Common;
    using ProcessingTools.Net.Factories.Contracts;

    /// <summary>
    /// Request data from EXTRACT.
    /// <see cref="https://extract.hcmr.gr/"/>
    /// </summary>
    public class ExtractHcmrDataRequester : IExtractHcmrDataRequester
    {
        private const string BaseAddress = "http://tagger.jensenlab.org/";
        private const string GetEntitiesApiUrl = "GetEntities";
        private readonly INetConnectorFactory connectorFactory;

        public ExtractHcmrDataRequester(INetConnectorFactory connectorFactory)
        {
            if (connectorFactory == null)
            {
                throw new ArgumentNullException(nameof(connectorFactory));
            }

            this.connectorFactory = connectorFactory;
        }

        public async Task<ExtractHcmrResponseModel> RequestData(string content)
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
            var result = await connector.PostAndDeserializeXml<ExtractHcmrResponseModel>(GetEntitiesApiUrl, values, Defaults.DefaultEncoding);
            return result;
        }
    }
}
