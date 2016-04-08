namespace ProcessingTools.Infrastructure.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public class Connector
    {
        public const string CorsHeaderName = "Access-Control-Allow-Origin";
        public const string CorsHeaderDefaultValue = "*";

        public const string DefaultContentType = "text/plain; encoding='utf-8'";
        public const string JsonContentType = "application/json";
        public const string XmlContentType = "application/xml";
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        private string baseAddress;

        public Connector()
        {
            this.BaseAddressUri = null;
        }

        public Connector(string baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        public string BaseAddress
        {
            get
            {
                return this.baseAddress;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(BaseAddress));
                }

                this.baseAddress = value;
                this.BaseAddressUri = new Uri(this.baseAddress);
            }
        }

        public Uri BaseAddressUri { get; private set; }

        public async Task<T> GetAndDeserializeXmlAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlContentType));

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var stream = await client.GetStreamAsync(url);
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stream);
            }

            return result;
        }

        public async Task<T> GetAndDeserializeDataContractJsonAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonContentType));

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var stream = await client.GetStreamAsync(url);
                var serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(stream);
            }

            return result;
        }

        public async Task<string> GetAsync(string url, string acceptContentType)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);

                if (!string.IsNullOrWhiteSpace(acceptContentType))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));
                }

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                return await client.GetStringAsync(url);
            }
        }

        public async Task<string> PostAsync(string url, string content, string contentType, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            using (var client = new HttpClient())
            using (var postContent = new StringContent(content, encoding))
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);

                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    postContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var response = await client.PostAsync(url, postContent);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PostAsync(string url, IDictionary<string, string> values, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            using (var client = new HttpClient())
            using (HttpContent content = new WeakFormUrlEncodedContent(values, encoding))
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var response = await client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PostAndDeserializeXmlAsync<T>(string url, Dictionary<string, string> values, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            using (var client = new HttpClient())
            using (HttpContent content = new WeakFormUrlEncodedContent(values, encoding))
            {
                client.DefaultRequestHeaders.Add(CorsHeaderName, CorsHeaderDefaultValue);

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var response = await client.PostAsync(url, content);
                var stream = await response.Content.ReadAsStreamAsync();
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}