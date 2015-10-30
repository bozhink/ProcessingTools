namespace ProcessingTools.SystemCommons
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    public class WeakFormUrlEncodedContent : ByteArrayContent
    {
        public WeakFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection, Encoding encoding)
            : base(WeakFormUrlEncodedContent.GetContentByteArray(nameValueCollection, encoding))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection, Encoding encoding)
        {
            if (nameValueCollection == null)
            {
                throw new ArgumentNullException("nameValueCollection");
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> current in nameValueCollection)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }

                stringBuilder.Append(WeakFormUrlEncodedContent.Encode(current.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(WeakFormUrlEncodedContent.Encode(current.Value));
            }

            return encoding.GetBytes(stringBuilder.ToString());
        }

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }

            return WebUtility.UrlEncode(data).Replace("%20", "+");
        }
    }
}
