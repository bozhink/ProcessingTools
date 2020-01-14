// <copyright file="HttpRequester.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Net
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
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Net;

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

        private HttpClient HttpClient => new HttpClient();

        private Encoding Encoding => Defaults.Encoding;

        /// <inheritdoc/>
        public Task<string> GetStringAsync(string requestUri)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetStringInternalAsync(new Uri(requestUri));
        }

        /// <inheritdoc/>
        public Task<string> GetStringAsync(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetStringInternalAsync(requestUri);
        }

        /// <inheritdoc/>
        public Task<string> GetStringAsync(string requestUri, string acceptContentType)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (string.IsNullOrWhiteSpace(acceptContentType))
            {
                throw new ArgumentNullException(nameof(acceptContentType));
            }

            return this.GetStringInternalAsync(new Uri(requestUri), acceptContentType);
        }

        /// <inheritdoc/>
        public Task<string> GetStringAsync(Uri requestUri, string acceptContentType)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (string.IsNullOrWhiteSpace(acceptContentType))
            {
                throw new ArgumentNullException(nameof(acceptContentType));
            }

            return this.GetStringInternalAsync(requestUri, acceptContentType);
        }

        /// <inheritdoc/>
        public Task<string> GetJsonAsync(string requestUri)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetJsonInternalAsync(new Uri(requestUri));
        }

        /// <inheritdoc/>
        public Task<string> GetJsonAsync(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetJsonInternalAsync(requestUri);
        }

        /// <inheritdoc/>
        public Task<T> GetJsonToObjectAsync<T>(string requestUri)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetJsonToObjectInternalAsync<T>(new Uri(requestUri));
        }

        /// <inheritdoc/>
        public Task<T> GetJsonToObjectAsync<T>(Uri requestUri)
            where T : class
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetJsonToObjectInternalAsync<T>(requestUri);
        }

        /// <inheritdoc/>
        public Task<string> GetXmlAsync(string requestUri)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetXmlInternalAsync(new Uri(requestUri));
        }

        /// <inheritdoc/>
        public Task<string> GetXmlAsync(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetXmlInternalAsync(requestUri);
        }

        /// <inheritdoc/>
        public Task<T> GetXmlToObjectAsync<T>(string requestUri)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetXmlToObjectInternalAsync<T>(new Uri(requestUri));
        }

        /// <inheritdoc/>
        public Task<T> GetXmlToObjectAsync<T>(Uri requestUri)
            where T : class
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            return this.GetXmlToObjectInternalAsync<T>(requestUri);
        }

        /// <inheritdoc/>
        public Task<string> PostAsync(string requestUri, string content, string contentType) => this.PostAsync(requestUri, content, contentType, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostAsync(Uri requestUri, string content, string contentType) => this.PostAsync(requestUri, content, contentType, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostAsync(string requestUri, string content, string contentType, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            HttpContent httpContent = this.GetContent(content, contentType, encoding);

            return this.PostInternalAsync(new Uri(requestUri), httpContent);
        }

        /// <inheritdoc/>
        public Task<string> PostAsync(Uri requestUri, string content, string contentType, Encoding encoding)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            HttpContent httpContent = this.GetContent(content, contentType, encoding);

            return this.PostInternalAsync(requestUri, httpContent);
        }

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(string requestUri, IDictionary<string, string> values) => this.PostToStringAsync(requestUri, values, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(Uri requestUri, IDictionary<string, string> values) => this.PostToStringAsync(requestUri, values, this.Encoding);

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(string requestUri, IDictionary<string, string> values, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            HttpContent httpContent = this.GetContent(values, encoding);

            return this.PostInternalAsync(new Uri(requestUri), httpContent);
        }

        /// <inheritdoc/>
        public Task<string> PostToStringAsync(Uri requestUri, IDictionary<string, string> values, Encoding encoding)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            HttpContent httpContent = this.GetContent(values, encoding);

            return this.PostInternalAsync(requestUri, httpContent);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(string requestUri, IDictionary<string, string> values)
            where T : class
        {
            return this.PostToXmlToObjectAsync<T>(requestUri, values, this.Encoding);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(Uri requestUri, IDictionary<string, string> values)
            where T : class
        {
            return this.PostToXmlToObjectAsync<T>(requestUri, values, this.Encoding);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(string requestUri, IDictionary<string, string> values, Encoding encoding)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            HttpContent httpContent = this.GetContent(values, encoding);

            return this.PostXmlToObjectInternalAsync<T>(new Uri(requestUri), httpContent);
        }

        /// <inheritdoc/>
        public Task<T> PostToXmlToObjectAsync<T>(Uri requestUri, IDictionary<string, string> values, Encoding encoding)
            where T : class
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            HttpContent httpContent = this.GetContent(values, encoding);

            return this.PostXmlToObjectInternalAsync<T>(requestUri, httpContent);
        }

        private async Task<string> GetStringInternalAsync(Uri requestUri)
        {
            var client = this.HttpClient.AddCorsHeader();

            return await client.GetStringAsync(requestUri).ConfigureAwait(false);
        }

        private async Task<string> GetStringInternalAsync(Uri requestUri, string acceptContentType)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptContentTypeHeader(acceptContentType);

            return await client.GetStringAsync(requestUri).ConfigureAwait(false);
        }

        private async Task<string> GetJsonInternalAsync(Uri requestUri)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptJsonHeader();

            return await client.GetStringAsync(requestUri).ConfigureAwait(false);
        }

        private async Task<T> GetJsonToObjectInternalAsync<T>(Uri requestUri)
            where T : class
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptJsonHeader();

            var stream = await client.GetStreamAsync(requestUri).ConfigureAwait(false);

            var serializer = new DataContractJsonSerializer(typeof(T));

            return (T)serializer.ReadObject(stream);
        }

        private async Task<string> GetXmlInternalAsync(Uri requestUri)
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptXmlHeader();

            return await client.GetStringAsync(requestUri).ConfigureAwait(false);
        }

        private async Task<T> GetXmlToObjectInternalAsync<T>(Uri requestUri)
            where T : class
        {
            var client = this.HttpClient.AddCorsHeader().AddAcceptXmlHeader();

            var stream = await client.GetStreamAsync(requestUri).ConfigureAwait(false);

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

        private HttpContent GetContent(string content, string contentType, Encoding encoding)
        {
            HttpContent httpContent = new StringContent(content, encoding);
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            }

            return httpContent;
        }

        private HttpContent GetContent(IDictionary<string, string> values, Encoding encoding)
        {
            return new WeakFormUrlEncodedContent(values, encoding);
        }
    }
}
