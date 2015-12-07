namespace ProcessingTools.Infrastructure.Net
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    public partial class Connector
    {
        public static async Task<string> GetJsonAsync(string url)
        {
            return await GetJsonAsync(baseAddressUri: null, url: url);
        }

        public static async Task<string> GetJsonAsync(string baseAddress, string url)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetJsonAsync(baseAddressUri: new Uri(baseAddress), url: url);
        }

        public static async Task<string> GetJsonAsync(Uri baseAddressUri, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                if (baseAddressUri != null)
                {
                    client.BaseAddress = baseAddressUri;
                }

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<string> GetJsonAsync(string baseAddress, string urlStringFormat, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetJsonAsync(new Uri(baseAddress), urlStringFormat, parameters);
        }

        public static async Task<string> GetJsonAsync(Uri baseAddressUri, string urlStringFormat, params object[] parameters)
        {
            if (string.IsNullOrEmpty(urlStringFormat))
            {
                throw new ArgumentNullException("urlStringFormat");
            }

            if (parameters == null || parameters.Length < 1)
            {
                throw new ArgumentNullException("parameters");
            }

            var urlParameters = parameters
                .Select(p => Uri.EscapeDataString(Uri.UnescapeDataString(p.ToString())))
                .ToArray();

            string url = string.Format(urlStringFormat, urlParameters);
            return await GetJsonAsync(baseAddressUri, url);
        }

        public static async Task<string> GetXmlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<string> GetXmlAsync(string baseAddress, string url)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetXmlAsync(new Uri(baseAddress), url);
        }

        public static async Task<string> GetXmlAsync(Uri baseAddress, string url)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException("baseAddress");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<XmlDocument> GetToXmlAsync(string url)
        {
            var response = await Connector.GetXmlAsync(url);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }

        public async Task<T> GetDeserializedXmlAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));

                var stream = await client.GetStreamAsync(url);
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stream);
            }

            return result;
        }

        public async Task<T> GetDeserializedDataContractJsonAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

                var stream = await client.GetStreamAsync(url);
                var serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(stream);
            }

            return result;
        }
    }
}