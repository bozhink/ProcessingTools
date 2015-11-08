namespace ProcessingTools.Infrastructure.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class Connector
    {
        public const string DefaultContentType = "text/plain; encoding='utf-8'";
        public const string JsonMediaType = "application/json";
        public const string XmlMediaType = "application/xml";
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        #region Get

        #region GetJson

        public static async Task<string> GetJsonAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<string> GetJsonAsync(string baseAddress, string url)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetJsonAsync(new Uri(baseAddress), url);
        }

        public static async Task<string> GetJsonAsync(Uri baseAddress, string url)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException("baseAddress");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<string> GetJsonAsync(string baseAddress, string urlStringFormat, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetJsonAsync(new Uri(baseAddress), urlStringFormat, parameters);
        }

        public static async Task<string> GetJsonAsync(Uri baseAddress, string urlStringFormat, params object[] parameters)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException("baseAddress");
            }

            if (string.IsNullOrEmpty(urlStringFormat))
            {
                throw new ArgumentNullException("urlStringFormat");
            }

            if (parameters == null || parameters.Length < 1)
            {
                throw new ArgumentNullException("parameters");
            }

            var urlParameters = parameters
                .Select(p => Uri.EscapeDataString(Uri.UnescapeDataString(p.ToString())))
                .ToArray();

            string url = string.Format(urlStringFormat, urlParameters);
            return await GetJsonAsync(baseAddress, url);
        }

        #endregion GetJson

        #region GetXml

        public static async Task<string> GetXmlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<string> GetXmlAsync(string baseAddress, string url)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentNullException("baseAddress");
            }

            return await GetXmlAsync(new Uri(baseAddress), url);
        }

        public static async Task<string> GetXmlAsync(Uri baseAddress, string url)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException("baseAddress");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(XmlMediaType));
                return await client.GetStringAsync(url);
            }
        }

        public static async Task<XmlDocument> GetXmlDocumentAsync(string url)
        {
            var response = await Connector.GetXmlAsync(url);

            XmlDocument xml = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xml.LoadXml(response);
            return xml;
        }

        #endregion GetXml

        #endregion Get

        #region Post

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

        #endregion Post
    }
}
