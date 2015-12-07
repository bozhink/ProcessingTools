namespace ProcessingTools.Infrastructure.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public partial class Connector
    {
        public static async Task<string> PostToStringAsync(string url, string content, string contentType = DefaultContentType)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

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

        public static async Task<XmlDocument> PostToXmlAsync(string url, string content, string contentType = DefaultContentType)
        {
            string response = await PostToStringAsync(url, content, contentType);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }

        public static async Task<string> PostUrlEncodedToStringAsync(string url, Dictionary<string, string> values, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            using (var client = new HttpClient())
            using (HttpContent content = new WeakFormUrlEncodedContent(values, Connector.DefaultEncoding))
            {
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }

        public static async Task<XmlDocument> PostUrlEncodedToXmlAsync(string url, Dictionary<string, string> values, Encoding encoding)
        {
            var response = await Connector.PostUrlEncodedToStringAsync(url, values, encoding);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }
    }
}