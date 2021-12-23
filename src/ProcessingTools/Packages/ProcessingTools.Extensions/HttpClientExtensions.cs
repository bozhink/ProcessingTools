﻿// <copyright file="HttpClientExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// Extensions for <see cref="HttpClient"/>.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Adds CORS headers.
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> instance.</param>
        /// <returns>Updated client.</returns>
        public static HttpClient AddCorsHeader(this HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.DefaultRequestHeaders.Add(HttpConstants.CorsHeaderName, HttpConstants.CorsHeaderDefaultValue);

            return client;
        }

        /// <summary>
        /// Adds accept-content-type header.
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> instance.</param>
        /// <param name="contentType">Content-type to be added.</param>
        /// <returns>Updated client.</returns>
        public static HttpClient AddAcceptContentTypeHeader(this HttpClient client, string contentType)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            }

            return client;
        }

        /// <summary>
        /// Adds accept-type=xml header.
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> instance.</param>
        /// <returns>Updated client.</returns>
        public static HttpClient AddAcceptXmlHeader(this HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.AddAcceptContentTypeHeader(ContentTypes.Xml);

            return client;
        }

        /// <summary>
        /// Adds accept-type=json header.
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/> instance.</param>
        /// <returns>Updated client.</returns>
        public static HttpClient AddAcceptJsonHeader(this HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.AddAcceptContentTypeHeader(ContentTypes.Json);

            return client;
        }
    }
}
