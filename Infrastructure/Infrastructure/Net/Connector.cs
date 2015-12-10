namespace ProcessingTools.Infrastructure.Net
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    public partial class Connector
    {
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
                    throw new ArgumentNullException("BaseAddress");
                }

                this.baseAddress = value;
                this.BaseAddressUri = new Uri(this.baseAddress);
            }
        }

        public Uri BaseAddressUri { get; private set; }

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

        public async Task<string> GetXmlStringAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));
                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                return await client.GetStringAsync(url);
            }
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

        public async Task<string> GetJsonStringAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
                if (this.BaseAddressUri != null)
                {
                    client.BaseAddress = this.BaseAddressUri;
                }

                return await client.GetStringAsync(url);
            }
        }
    }
}