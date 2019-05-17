// <copyright file="HttpRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Default implementation of <see cref="IHttpRequester"/>.
    /// </summary>
    public class HttpRequester : IHttpRequester
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequester"/> class.
        /// </summary>
        public HttpRequester()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequester"/> class.
        /// </summary>
        /// <param name="baseAddress">Base address for the requested API.</param>
        public HttpRequester(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }

            this.BaseAddressUri = new Uri(baseAddress);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequester"/> class.
        /// </summary>
        /// <param name="baseAddress">Base address for the requested API.</param>
        public HttpRequester(Uri baseAddress)
        {
            this.BaseAddressUri = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
        }

        private Uri BaseAddressUri { get; }

        private HttpClient HttpClient => new HttpClient();

        private Encoding Encoding => ProcessingTools.Common.Constants.Defaults.Encoding;

        /// <inheritdoc/>
        public Task<string> GetStringAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return this.GetStringInternalAsync(url);
        }

        /// <inheritdoc/>
        public Task<string> GetStringAsync(string url, string acceptContentType)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(acceptContentType))
            {
                throw new ArgumentNullException(nameof(acceptContentType));
            }

            return this.GetStringInternalAsync(url, acceptContentType);
        }

        /// <inheritdoc/>
        public Task<string> GetJsonAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return this.GetJsonInternalAsync(url);
        }

        /// <inheritdoc/>
        public Task<T> GetJsonToObjectAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return this.GetJsonToObjectInternalAsync<T>(url);
        }

        /// <inheritdoc/>
        public Task<string> GetXmlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return this.GetXmlInternalAsync(url);
        }

        /// <inheritdoc/>
        public Task<T> GetXmlToObjectAsync<T>(string url)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return this.GetXmlToObjectInternalAsync<T>(url);
        }

        /// <inheritdoc/>
        public Task<string> PostAsync(string url, string content, string contentType) => this.PostAsync(url, content, contentType, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostAsync(string url, string content, string contentType, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            Uri requestUri = this.GetRequestUri(this.BaseAddressUri, url);

            HttpContent httpContent = new StringContent(content, encoding);
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            return this.PostInternalAsync(requestUri, httpContent);
        }

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(string url, IDictionary<string, string> values) => this.PostToStringAsync(url, values, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(string url, IDictionary<string, string> values, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Uri requestUri = this.GetRequestUri(this.BaseAddressUri, url);

            HttpContent httpContent = new WeakFormUrlEncodedContent(values, encoding);

            return this.PostInternalAsync(requestUri, httpContent);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(string url, IDictionary<string, string> values)
            where T : class
        {
            return this.PostToXmlToObjectAsync<T>(url, values, this.Encoding);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(string url, IDictionary<string, string> values, Encoding encoding)
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

            Uri requestUri = this.GetRequestUri(this.BaseAddressUri, url);

            HttpContent httpContent = new WeakFormUrlEncodedContent(values, encoding);

            return this.PostXmlToObjectInternalAsync<T>(requestUri, httpContent);
        }

        private async Task<string> GetStringInternalAsync(string url)
        {
            var client = this.HttpClient.AddCorsHeader();

            return await this.GetStringAsync(client, url).ConfigureAwait(false);
        }

        private async Task<string> GetStringInternalAsync(string url, string acceptContentType)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptContentTypeHeader(acceptContentType);

            return await this.GetStringAsync(client, url).ConfigureAwait(false);
        }

        private async Task<string> GetJsonInternalAsync(string url)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptJsonHeader();

            return await this.GetStringAsync(client, url).ConfigureAwait(false);
        }

        private async Task<T> GetJsonToObjectInternalAsync<T>(string url)
            where T : class
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptJsonHeader();

            var stream = await this.GetStreamAsync(client, url).ConfigureAwait(false);

            var serializer = new DataContractJsonSerializer(typeof(T));

            return (T)serializer.ReadObject(stream);
        }

        private async Task<string> GetXmlInternalAsync(string url)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptXmlHeader();

            return await this.GetStringAsync(client, url).ConfigureAwait(false);
        }

        private async Task<T> GetXmlToObjectInternalAsync<T>(string url)
            where T : class
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptXmlHeader();

            var stream = await this.GetStreamAsync(client, url).ConfigureAwait(false);

            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(stream);
        }

        private async Task<string> PostInternalAsync(Uri requestUri, HttpContent httpContent)
        {
            var client = this.HttpClient.AddCorsHeader();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = httpContent,
            };

            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            return await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task<Stream> PostInternalAsync(Uri requestUri, HttpContent httpContent, string acceptContentType)
        {
            var client = this.HttpClient.AddCorsHeader();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = httpContent,
            };

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));

            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            return await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private async Task<T> PostXmlToObjectInternalAsync<T>(Uri requestUri, HttpContent httpContent)
            where T : class
        {
            var stream = await this.PostInternalAsync(requestUri, httpContent, ContentTypes.Xml).ConfigureAwait(false);

            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(stream);
        }

        private Uri GetRequestUri(string uri) => new Uri(uri);

        private Uri GetRequestUri(Uri uri) => uri;

        private Uri GetRequestUri(string baseUri, string relaticeUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                return new Uri(relaticeUri);
            }
            else
            {
                return new Uri(new Uri(baseUri), relaticeUri);
            }
        }

        private Uri GetRequestUri(Uri baseUri, string relaticeUri)
        {
            if (baseUri == null)
            {
                return new Uri(relaticeUri);
            }
            else
            {
                return new Uri(baseUri, relaticeUri);
            }
        }

        private Task<string> GetStringAsync(HttpClient client, string url)
        {
            if (this.BaseAddressUri != null)
            {
                client.BaseAddress = this.BaseAddressUri;
            }

            return client.GetStringAsync(url);
        }

        private Task<Stream> GetStreamAsync(HttpClient client, string url)
        {
            if (this.BaseAddressUri != null)
            {
                client.BaseAddress = this.BaseAddressUri;
            }

            return client.GetStreamAsync(url);
        }
    }
}
