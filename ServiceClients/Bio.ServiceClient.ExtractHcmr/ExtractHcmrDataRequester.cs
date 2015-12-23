namespace ProcessingTools.Bio.ServiceClient.ExtractHcmr
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Net;
    using Models;

    /// <summary>
    /// Request data from EXTRACT.
    /// <see cref="https://extract.hcmr.gr/"/>
    /// </summary>
    public class ExtractHcmrDataRequester : IExtractHcmrDataRequester
    {
        private const string BaseAddress = "http://tagger.jensenlab.org/";
        private const string GetEntitiesApiUrl = "GetEntities";
        private readonly Encoding defaultEncoding = Encoding.UTF8;

        public async Task<ExtractHcmrResponseModel> RequestData(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("Content string to send is empty.");
            }

            var values = new Dictionary<string, string>
            {
                { "document", content },
                { "entity_types", "-25 -26 -27" },
                { "format", "xml" }
            };

            try
            {
                var connector = new Connector(BaseAddress);
                var result = await connector.PostAndDeserializeXmlAsync<ExtractHcmrResponseModel>(GetEntitiesApiUrl, values, this.defaultEncoding);
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
