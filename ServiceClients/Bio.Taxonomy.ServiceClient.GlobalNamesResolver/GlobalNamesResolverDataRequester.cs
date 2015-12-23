namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Extensions;
    using Infrastructure.Net;

    public class GlobalNamesResolverDataRequester
    {
        private const string BaseAddress = "http://resolver.globalnames.org";
        private const string ApiUrl = "name_resolvers.xml";
        private readonly Encoding encoding = Encoding.UTF8;

        public async Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string searchString = this.BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                string url = $"{ApiUrl}?{searchString}";

                var connector = new Connector(BaseAddress);
                string response = await connector.GetAsync(url, Connector.XmlContentType);
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

                var connector = new Connector(BaseAddress);
                var response = await connector.PostAsync(ApiUrl, postData, contentType, encoding);
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
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("data", string.Join("\r\n", scientificNames));

                if (sourceId != null)
                {
                    values.Add("data_source_ids", string.Join("|", sourceId));
                }

                var connector = new Connector(BaseAddress);
                var response = await connector.PostAsync(ApiUrl, values, this.encoding);
                return response.ToXmlDocument();
            }
            catch
            {
                throw;
            }
        }

        private string BuildGlobalNamesResolverSearchString(string[] scientificNames, int[] sourceId)
        {
            StringBuilder searchStringBuilder = new StringBuilder();
            searchStringBuilder.Append("names=");

            if (scientificNames != null && scientificNames.Length > 0)
            {
                Regex whiteSpaces = new Regex(@"\s+");
                searchStringBuilder.Append(string.Join("|", scientificNames.Select(s => whiteSpaces.Replace(s.Trim(), "+"))));
            }
            else
            {
                throw new ArgumentNullException("scientificNames should be a non-empty array of strings.");
            }

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