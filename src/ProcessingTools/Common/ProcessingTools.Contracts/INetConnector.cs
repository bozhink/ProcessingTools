// <copyright file="INetConnector.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Default requester for Internet information
    /// </summary>
    public interface INetConnector
    {
        /// <summary>
        /// Asynchronously GET string from specified URL.
        /// </summary>
        /// <param name="url">URL to be requested</param>
        /// <param name="acceptContentType">Accept content-type</param>
        /// <returns>The response as string</returns>
        Task<string> GetAsync(string url, string acceptContentType);

        /// <summary>
        /// Asynchronously GET and deserialize string as JSON from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result</typeparam>
        /// <param name="url">URL to be requested</param>
        /// <returns>Task of deserialized as JSON response string</returns>
        Task<T> GetJsonObjectAsync<T>(string url)
            where T : class;

        /// <summary>
        /// Asynchronously GET and deserialize string as XML from specified URL.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result</typeparam>
        /// <param name="url">URL to be requested</param>
        /// <returns>Task of deserialized as XML response string</returns>
        Task<T> GetXmlObjectAsync<T>(string url)
            where T : class;

        /// <summary>
        /// Asynchronously POST content to URL.
        /// </summary>
        /// <param name="url">URL to be requested</param>
        /// <param name="content">Content to be sent as string</param>
        /// <param name="contentType">Content-Type of the content</param>
        /// <param name="encoding">Encoding of the content string</param>
        /// <returns>Task of response string</returns>
        Task<string> PostAsync(string url, string content, string contentType, Encoding encoding);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL.
        /// </summary>
        /// <param name="url">URL to be requested</param>
        /// <param name="values">Dictionary of data as key-value pairs</param>
        /// <param name="encoding">Encoding of the content string</param>
        /// <returns>Task of response string</returns>
        Task<string> PostAsync(string url, IDictionary<string, string> values, Encoding encoding);

        /// <summary>
        /// Asynchronously POST key-value pairs data to URL and deserialize response string as XML.
        /// </summary>
        /// <typeparam name="T">Type of the deserialized result</typeparam>
        /// <param name="url">URL to be requested</param>
        /// <param name="values">Dictionary of data as key-value pairs</param>
        /// <param name="encoding">Encoding of the content string</param>
        /// <returns>Task of deserialized as XML response string</returns>
        Task<T> PostXmlObjectAsync<T>(string url, IDictionary<string, string> values, Encoding encoding)
            where T : class;
    }
}
