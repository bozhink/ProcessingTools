namespace ProcessingTools.Common.Extensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ProcessingTools.Constants;

    public static class HttpClientExtensions
    {
        public static HttpClient AddCorsHeader(this HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.DefaultRequestHeaders.Add(
                HttpConstants.CorsHeaderName,
                HttpConstants.CorsHeaderDefaultValue);

            return client;
        }

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

        public static HttpClient AddAcceptXmlHeader(this HttpClient client)
        {
            client.AddAcceptContentTypeHeader(ContentTypes.Xml);

            return client;
        }

        public static HttpClient AddAcceptJsonHeader(this HttpClient client)
        {
            client.AddAcceptContentTypeHeader(ContentTypes.Json);

            return client;
        }
    }
}
