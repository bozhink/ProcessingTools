namespace ProcessingTools.Net.Extensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ProcessingTools.Constants;
    using ProcessingTools.Net.Constants;

    public static class ClientExtensions
    {
        public static void AddCorsHeader(this HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.DefaultRequestHeaders.Add(
                HeaderConstants.CorsHeaderName,
                HeaderConstants.CorsHeaderDefaultValue);
        }

        public static void AddAcceptContentTypeHeader(this HttpClient client, string contentType)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            }
        }

        public static void AddAcceptXmlHeader(this HttpClient client)
        {
            client.AddAcceptContentTypeHeader(ContentTypes.Xml);
        }

        public static void AddAcceptJsonHeader(this HttpClient client)
        {
            client.AddAcceptContentTypeHeader(ContentTypes.Json);
        }
    }
}
