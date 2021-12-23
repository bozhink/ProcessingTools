// <copyright file="IHttpRequester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// HTTP requester - object to request information via HTTP protocol.
    /// </summary>
    public interface IHttpRequester
    {
        /// <summary>
        /// Asynchronously GET string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response as string.</returns>
        Task<string> GetStringAsync(string requestUri);

        /// <summary>
        /// Asynchronously GET string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response as string.</returns>
        Task<string> GetStringAsync(Uri requestUri);

        /// <summary>
        /// Asynchronously GET string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="acceptContentType">Accept content-type.</param>
        /// <returns>The response as string.</returns>
        Task<string> GetStringAsync(string requestUri, string acceptContentType);

        /// <summary>
        /// Asynchronously GET string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="acceptContentType">Accept content-type.</param>
        /// <returns>The response as string.</returns>
        Task<string> GetStringAsync(Uri requestUri, string acceptContentType);

        /// <summary>
        /// Asynchronously GET JSON string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response JSON string.</returns>
        Task<string> GetJsonAsync(string requestUri);

        /// <summary>
        /// Asynchronously GET JSON string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response JSON string.</returns>
        Task<string> GetJsonAsync(Uri requestUri);

        /// <summary>
        /// Asynchronously GET and deserialize JSON string to object from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the resultant deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>Deserialized object from the response JSON string.</returns>
        Task<T> GetJsonToObjectAsync<T>(string requestUri)
            where T : class;

        /// <summary>
        /// Asynchronously GET and deserialize JSON string to object from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the resultant deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>Deserialized object from the response JSON string.</returns>
        Task<T> GetJsonToObjectAsync<T>(Uri requestUri)
            where T : class;

        /// <summary>
        /// Asynchronously GET XML string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response XML string.</returns>
        Task<string> GetXmlAsync(string requestUri);

        /// <summary>
        /// Asynchronously GET XML string from specified URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>The response XML string.</returns>
        Task<string> GetXmlAsync(Uri requestUri);

        /// <summary>
        /// Asynchronously GET and deserialize XML string to object from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the resultant deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>Deserialized object from the response XML string.</returns>
        Task<T> GetXmlToObjectAsync<T>(string requestUri)
            where T : class;

        /// <summary>
        /// Asynchronously GET and deserialize XML string to object from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the resultant deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <returns>Deserialized object from the response XML string.</returns>
        Task<T> GetXmlToObjectAsync<T>(Uri requestUri)
            where T : class;

        /// <summary>
        /// Asynchronously POST string content to URL with default encoding.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="content">Content to be sent as string.</param>
        /// <param name="contentType">Content-Type of the content.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostAsync(string requestUri, string content, string contentType);

        /// <summary>
        /// Asynchronously POST string content to URL with default encoding.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="content">Content to be sent as string.</param>
        /// <param name="contentType">Content-Type of the content.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostAsync(Uri requestUri, string content, string contentType);

        /// <summary>
        /// Asynchronously POST string content to URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="content">Content to be sent as string.</param>
        /// <param name="contentType">Content-Type of the content.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostAsync(string requestUri, string content, string contentType, Encoding encoding);

        /// <summary>
        /// Asynchronously POST string content to URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="content">Content to be sent as string.</param>
        /// <param name="contentType">Content-Type of the content.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostAsync(Uri requestUri, string content, string contentType, Encoding encoding);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL with default encoding.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostToStringAsync(string requestUri, IDictionary<string, string> values);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL with default encoding.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostToStringAsync(Uri requestUri, IDictionary<string, string> values);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostToStringAsync(string requestUri, IDictionary<string, string> values, Encoding encoding);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL.
        /// </summary>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Task of response string.</returns>
        Task<string> PostToStringAsync(Uri requestUri, IDictionary<string, string> values, Encoding encoding);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL with default encoding and deserialize response XML string to object.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <returns>Deserialize response XML string to object.</returns>
        Task<T> PostToXmlToObjectAsync<T>(string requestUri, IDictionary<string, string> values)
            where T : class;

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL with default encoding and deserialize response XML string to object.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <returns>Deserialize response XML string to object.</returns>
        Task<T> PostToXmlToObjectAsync<T>(Uri requestUri, IDictionary<string, string> values)
            where T : class;

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL and deserialize response XML string to object.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Deserialize response XML string to object.</returns>
        Task<T> PostToXmlToObjectAsync<T>(string requestUri, IDictionary<string, string> values, Encoding encoding)
            where T : class;

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL and deserialize response XML string to object.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result.</typeparam>
        /// <param name="requestUri">URI to be requested.</param>
        /// <param name="values">Dictionary of data as key-value pairs.</param>
        /// <param name="encoding">Encoding of the content string.</param>
        /// <returns>Deserialize response XML string to object.</returns>
        Task<T> PostToXmlToObjectAsync<T>(Uri requestUri, IDictionary<string, string> values, Encoding encoding)
            where T : class;
    }
}
