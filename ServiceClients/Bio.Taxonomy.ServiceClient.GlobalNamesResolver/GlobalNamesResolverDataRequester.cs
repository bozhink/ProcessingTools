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
        private const string GlobalNamesResolverBaseAddress = "http://resolver.globalnames.org";

        public static async Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string searchString = BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                string url = $"name_resolvers.xml?{searchString}";

                var connector = new Connector(GlobalNamesResolverBaseAddress);
                string response = await connector.GetXmlStringAsync(url);
                return response.ToXmlDocument();
            }
            catch
            {
                throw;
            }
        }

        public static async Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId = null)
        {
            const string ApiUrl = "http://resolver.globalnames.org/name_resolvers.xml";
            try
            {
                string postData = BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                string contentType = "application/x-www-form-urlencoded";

                return await Connector.PostToXmlAsync(ApiUrl, postData, contentType);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId = null)
        {
            const string ApiUrl = "http://resolver.globalnames.org/name_resolvers.xml";

            try
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("data", string.Join("\r\n", scientificNames));

                if (sourceId != null)
                {
                    values.Add("data_source_ids", string.Join("|", sourceId));
                }

                return await Connector.PostUrlEncodedToXmlAsync(ApiUrl, values, Encoding.UTF8);
            }
            catch
            {
                throw;
            }
        }

        private static string BuildGlobalNamesResolverSearchString(string[] scientificNames, int[] sourceId)
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
