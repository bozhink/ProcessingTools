// <copyright file="NetConnector.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

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
    using ProcessingTools.Contracts;

    /// <summary>
    /// Default implementation of <see cref="INetConnector"/>.
    /// </summary>
    public class NetConnector : INetConnector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetConnector"/> class.
        /// </summary>
        public NetConnector()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetConnector"/> class.
        /// </summary>
        /// <param name="baseAddress">Base address for the requested API.</param>
        public NetConnector(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }

            this.BaseAddressUri = new Uri(baseAddress);
        }

        private Uri BaseAddressUri { get; set; }

        /// <inheritdoc/>
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

                var stream = await client.GetStreamAsync(url).ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stream);
            }

            return result;
        }

        /// <inheritdoc/>
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

                var stream = await client.GetStreamAsync(url).ConfigureAwait(false);
                var serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(stream);
            }

            return result;
        }

        /// <inheritdoc/>
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

                return await client.GetStringAsync(url).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
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

                var response = await client.PostAsync(url, postContent).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
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

                var response = await client.PostAsync(url, content).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
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

                var response = await client.PostAsync(url, content).ConfigureAwait(false);
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
