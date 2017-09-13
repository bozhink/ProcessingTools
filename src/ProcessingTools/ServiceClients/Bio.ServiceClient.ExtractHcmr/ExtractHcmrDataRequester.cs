namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.ServiceClient.ExtractHcmr.Contracts;
    using ProcessingTools.Bio.ServiceClient.ExtractHcmr.Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Net;

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
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
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
            var result = await connector.PostXmlObjectAsync<ExtractHcmrResponseModel>(GetEntitiesApiUrl, values, Defaults.Encoding).ConfigureAwait(false);
            return result;
        }
    }
}
