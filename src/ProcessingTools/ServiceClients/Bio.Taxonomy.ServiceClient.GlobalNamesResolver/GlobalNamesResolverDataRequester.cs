namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Net.Factories.Contracts;

    public class GlobalNamesResolverDataRequester : IGlobalNamesResolverDataRequester
    {
        private const string BaseAddress = "http://resolver.globalnames.org";
        private const string ApiUrl = "name_resolvers.xml";
        private readonly INetConnectorFactory connectorFactory;

        public GlobalNamesResolverDataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        public async Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string searchString = this.BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                string url = $"{ApiUrl}?{searchString}";

                var connector = this.connectorFactory.Create(BaseAddress);
                string response = await connector.GetAsync(url, ContentTypes.Xml);
                return response.ToXmlDocument();
            }
            catch
            {
                throw;
            }
        }

        public async Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string postData = this.BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                string contentType = "application/x-www-form-urlencoded";

                var connector = this.connectorFactory.Create(BaseAddress);
                var response = await connector.PostAsync(ApiUrl, postData, contentType, Defaults.Encoding);
                return response.ToXmlDocument();
            }
            catch
            {
                throw;
            }
        }

        public async Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "data", string.Join("\r\n", scientificNames) }
                };

                if (sourceId != null)
                {
                    values.Add("data_source_ids", string.Join("|", sourceId));
                }

                var connector = this.connectorFactory.Create(BaseAddress);
                var response = await connector.PostAsync(ApiUrl, values, Defaults.Encoding);
                return response.ToXmlDocument();
            }
            catch
            {
                throw;
            }
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
