// <copyright file="EncodeHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Security
{
    using System;

    /// <summary>
    /// Encode helper.
    /// </summary>
    /// <remarks>
    /// See https://stackoverflow.com/questions/1228701/code-for-decoding-encoding-a-modified-base64-url
    /// See https://tools.ietf.org/html/draft-ietf-jose-json-web-signature-08#appendix-C
    /// See https://stackoverflow.com/questions/50731397/httpserverutility-urltokenencode-replacement-for-netstandard
    /// </remarks>
    public static class EncodeHelper
    {
        /// <summary>
        /// Encodes byte array to Base64 URL.
        /// </summary>
        /// <param name="source">Source byte array to be encoded.</param>
        /// <returns>Base 64 URL encoded string.</returns>
        /// <remarks>
        /// See https://tools.ietf.org/html/draft-ietf-jose-json-web-signature-08#appendix-C
        /// </remarks>
        public static string Base64UrlEncode(byte[] source)
        {
            string s = Convert.ToBase64String(source); // Regular base64 encoder
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding
            return s;
        }

        /// <summary>
        /// Decodes Base 64 URL string to byte array.
        /// </summary>
        /// <param name="source">Source string to be decoded.</param>
        /// <returns>Decoded byte array.</returns>
        /// <remarks>
        /// See https://tools.ietf.org/html/draft-ietf-jose-json-web-signature-08#appendix-C
        /// </remarks>
        public static byte[] Base64UrlDecode(string source)
        {
            string s = source;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding

            // Pad with trailing '='s
            switch (s.Length % 4)
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default: throw new InvalidOperationException("Illegal base64url string!");
            }

            return Convert.FromBase64String(s); // Standard base64 decoder
        }
    }
}
