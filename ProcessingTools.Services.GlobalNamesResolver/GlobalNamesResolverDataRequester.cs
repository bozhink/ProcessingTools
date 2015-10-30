namespace ProcessingTools.Services.GlobalNamesResolver
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    public class GlobalNamesResolverDataRequester
    {
        public static async Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string searchString = BuildGlobalNamesResolverSearchString(scientificNames, sourceId);

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://resolver.globalnames.org/name_resolvers.xml?" + searchString);

                    XmlDocument xml = new XmlDocument
                    {
                        PreserveWhitespace = true
                    };

                    xml.LoadXml(response);

                    return xml;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                string postData = BuildGlobalNamesResolverSearchString(scientificNames, sourceId);
                byte[] data = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://resolver.globalnames.org/name_resolvers.xml");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = await request.GetResponseAsync();

                XmlDocument xml = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                xml.Load(response.GetResponseStream());

                return xml;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("data", string.Join("\r\n", scientificNames));

                    if (sourceId != null)
                    {
                        values.Add("data_source_ids", string.Join("|", sourceId));
                    }

                    using (HttpContent content = new FormUrlEncodedContent(values))
                    {
                        var response = await client.PostAsync("http://resolver.globalnames.org/name_resolvers.xml", content);
                        var responseString = await response.Content.ReadAsStringAsync();

                        XmlDocument xml = new XmlDocument
                        {
                            PreserveWhitespace = true
                        };

                        xml.LoadXml(responseString);

                        return xml;
                    }
                }
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
