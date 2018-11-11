// <copyright file="WeakFormUrlEncodedContent.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// Form URL encoded content.
    /// </summary>
    public class WeakFormUrlEncodedContent : ByteArrayContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeakFormUrlEncodedContent"/> class.
        /// </summary>
        /// <param name="nameValueCollection">Collection of form items as name-value pairs.</param>
        /// <param name="encoding">Encoding for the resultant string.</param>
        public WeakFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection, Encoding encoding)
            : base(GetContentByteArray(nameValueCollection, encoding))
        {
            this.Headers.ContentType = new MediaTypeHeaderValue(ContentTypes.UrlEncoded);
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection, Encoding encoding)
        {
            if (nameValueCollection == null)
            {
                throw new ArgumentNullException(nameof(nameValueCollection));
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> current in nameValueCollection)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }

                stringBuilder.Append(Encode(current.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Encode(current.Value));
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
