namespace ProcessingTools.Net
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants;

    public static class Utilities
    {
        public static HttpRequestMessage GetPostJsonHttpRequestMessage(Uri requestUri, object model)
        {
            if (requestUri == null)
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

        public static async Task<HttpRequestResponseModel> DoHttpPostJsonRequestAsync(Uri requestUri, object model)
        {
            HttpRequestResponseModel httpRequestResponse = new HttpRequestResponseModel();

            HttpClient client = new HttpClient();

            httpRequestResponse.RequestMessage = GetPostJsonHttpRequestMessage(requestUri, model);

            httpRequestResponse.RequestIn = DateTime.Now;

            httpRequestResponse.ResponseMessage = await client.SendAsync(httpRequestResponse.RequestMessage).ConfigureAwait(false);

            httpRequestResponse.ResponseOut = DateTime.Now;

            if (httpRequestResponse.RequestMessage?.Content != null)
            {
                httpRequestResponse.RequestContent = await httpRequestResponse.RequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            if (httpRequestResponse.ResponseMessage?.Content != null)
            {
                httpRequestResponse.ResponseContent = await httpRequestResponse.ResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return httpRequestResponse;
        }

        public class HttpRequestResponseModel
        {
            public HttpRequestResponseModel()
            {
                this.RequestMessage = null;
                this.ResponseMessage = null;
                this.RequestContent = null;
                this.ResponseContent = null;
                this.RequestIn = DateTime.Now;
                this.ResponseOut = DateTime.Now;
            }

            public HttpRequestResponseModel(HttpRequestResponseModel model)
            {
                this.RequestMessage = model.RequestMessage;
                this.ResponseMessage = model.ResponseMessage;
                this.RequestContent = model.RequestContent;
                this.ResponseContent = model.ResponseContent;
                this.RequestIn = model.RequestIn;
                this.ResponseOut = model.ResponseOut;
            }

            public HttpRequestMessage RequestMessage { get; set; }

            public HttpResponseMessage ResponseMessage { get; set; }

            public string RequestContent { get; set; }

            public string ResponseContent { get; set; }

            public DateTime RequestIn { get; set; }

            public DateTime ResponseOut { get; set; }
        }
    }
}
