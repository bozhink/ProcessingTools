namespace ProcessingTools.Infrastructure.Net
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class Connector
    {
        public const string DefaultContentType = "text/plain; encoding='utf-8'";
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        public static async Task<string> GetStringAsync(string url)
        {
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                return response;
            }
        }

        public static async Task<XmlDocument> GetXmlAsync(string url)
        {
            var response = await Connector.GetStringAsync(url);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }

        public static async Task<string> PostStringAsync(string url, string content, string contentType = DefaultContentType)
        {
            try
            {
                byte[] bytes = Connector.DefaultEncoding.GetBytes(content);
                int length = bytes.Length;

                var request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentLength = length;
                request.ContentType = contentType;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, length);
                }

                using (var response = await request.GetResponseAsync())
                {
                    var stream = response.GetResponseStream();
                    using (var reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<XmlDocument> PostXmlAsync(string url, string content, string contentType = DefaultContentType)
        {
            XmlDocument response = new XmlDocument
            {
                PreserveWhitespace = true
            };

            response.LoadXml(await PostStringAsync(url, content, contentType));

            return response;
        }

        public static async Task<string> PostUrlEncodedStringAsync(string url, Dictionary<string, string> values, Encoding encoding)
        {
            using (var client = new HttpClient())
            using (HttpContent content = new WeakFormUrlEncodedContent(values, Connector.DefaultEncoding))
            {
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }

        public static async Task<XmlDocument> PostUrlEncodedXmlAsync(string url, Dictionary<string, string> values, Encoding encoding)
        {
            var response = await Connector.PostUrlEncodedStringAsync(url, values, encoding);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }
    }
}
