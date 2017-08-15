namespace ProcessingTools.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts.Net;

    public class NetConnector : INetConnector
    {
        private string baseAddress;

        public NetConnector()
        {
            this.baseAddress = null;
        }

        public NetConnector(string baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        private string BaseAddress
        {
            get
            {
                return this.baseAddress;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(value);
                }

                this.baseAddress = value;
                this.BaseAddressUri = new Uri(this.baseAddress);
            }
        }

        private Uri BaseAddressUri { get; set; }

        public async Task<T> GetXmlObjectAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.AddCorsHeader().AddAcceptXmlHeader();

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

        public async Task<T> GetJsonObjectAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            T result = null;
            using (var client = new HttpClient())
            {
                client.AddCorsHeader().AddAcceptJsonHeader();

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
                client.AddCorsHeader().AddAcceptContentTypeHeader(acceptContentType);

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
                client.AddCorsHeader();

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
                client.AddCorsHeader();

                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                var response = await client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<T> PostXmlObjectAsync<T>(string url, IDictionary<string, string> values, Encoding encoding)
            where T : class
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
                client.AddCorsHeader().AddAcceptXmlHeader();

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
