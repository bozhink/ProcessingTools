// <copyright file="Utilities.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Net
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// Net utilities.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Get POST as JSON HTTP request message.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="model">Data model to be serialized to JSON.</param>
        /// <returns>Request-response model.</returns>
        public static HttpRequestMessage GetPostJsonHttpRequestMessage(Uri requestUri, object model)
        {
            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            requestMessage.Headers.Add("cache-control", "no-cache");
            requestMessage.Headers.Add("Accept", "application/json");

            if (model != null)
            {
                string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                HttpContent content = new StringContent(json, Defaults.Encoding, "application/json");
                requestMessage.Content = content;
            }

            return requestMessage;
        }

        /// <summary>
        /// Do HTTP POST as JSON request.
        /// </summary>
        /// <param name="requestUri">The request URL.</param>
        /// <param name="model">Data model to be serialized to JSON.</param>
        /// <returns>Request-response model.</returns>
        public static async Task<HttpRequestResponseModel> DoHttpPostJsonRequestAsync(Uri requestUri, object model)
        {
            HttpRequestResponseModel httpRequestResponse = new HttpRequestResponseModel();

            HttpClient client = new HttpClient();

            httpRequestResponse.RequestMessage = GetPostJsonHttpRequestMessage(requestUri, model);

            if (httpRequestResponse.RequestMessage?.Content != null)
            {
                httpRequestResponse.RequestContent = await httpRequestResponse.RequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            httpRequestResponse.RequestIn = DateTime.UtcNow;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Trust all certificates
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            httpRequestResponse.ResponseMessage = await client.SendAsync(httpRequestResponse.RequestMessage).ConfigureAwait(false);

            httpRequestResponse.ResponseOut = DateTime.UtcNow;

            if (httpRequestResponse.ResponseMessage?.Content != null)
            {
                httpRequestResponse.ResponseContent = await httpRequestResponse.ResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return httpRequestResponse;
        }

        /// <summary>
        /// HTTP request and response model.
        /// </summary>
        public class HttpRequestResponseModel
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HttpRequestResponseModel"/> class.
            /// </summary>
            public HttpRequestResponseModel()
            {
                this.RequestMessage = null;
                this.ResponseMessage = null;
                this.RequestContent = null;
                this.ResponseContent = null;
                this.RequestIn = DateTime.UtcNow;
                this.ResponseOut = DateTime.UtcNow;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="HttpRequestResponseModel"/> class.
            /// </summary>
            /// <param name="model">Initialization values.</param>
            public HttpRequestResponseModel(HttpRequestResponseModel model)
            {
                this.RequestMessage = model.RequestMessage;
                this.ResponseMessage = model.ResponseMessage;
                this.RequestContent = model.RequestContent;
                this.ResponseContent = model.ResponseContent;
                this.RequestIn = model.RequestIn;
                this.ResponseOut = model.ResponseOut;
            }

            /// <summary>
            /// Gets or sets the request message.
            /// </summary>
            public HttpRequestMessage RequestMessage { get; set; }

            /// <summary>
            /// Gets or sets the response message.
            /// </summary>
            public HttpResponseMessage ResponseMessage { get; set; }

            /// <summary>
            /// Gets or sets the request content.
            /// </summary>
            public string RequestContent { get; set; }

            /// <summary>
            /// Gets or sets the response content.
            /// </summary>
            public string ResponseContent { get; set; }

            /// <summary>
            /// Gets or sets the date and time for the entering of the request.
            /// </summary>
            public DateTime RequestIn { get; set; }

            /// <summary>
            /// Gets or sets the date and time of the exiting of the response.
            /// </summary>
            public DateTime ResponseOut { get; set; }
        }
    }
}
