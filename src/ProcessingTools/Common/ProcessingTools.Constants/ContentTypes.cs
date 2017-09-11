// <copyright file="ContentTypes.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Constants
{
    /// <summary>
    /// Constants related to http content types.
    /// </summary>
    public static class ContentTypes
    {
        /// <summary>
        /// Default mime-subtype.
        /// </summary>
        public const string DefaultMimesubtype = "unknown";

        /// <summary>
        /// Default mime-subtype returned on exception.
        /// </summary>
        public const string DefaultMimesubtypeOnException = "octet-stream";

        /// <summary>
        /// Default mime-type.
        /// </summary>
        public const string DefaultMimetype = "unknown";

        /// <summary>
        /// Default mime-type returned on exception.
        /// </summary>
        public const string DefaultMimetypeOnException = "application";

        /// <summary>
        /// Default content type.
        /// </summary>
        public const string Default = "text/plain; encoding='utf-8'";

        /// <summary>
        /// JPEG
        /// </summary>
        public const string Jpeg = "image/jpeg";

        /// <summary>
        /// JSON
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        /// URL-encoded
        /// </summary>
        public const string UrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// XML
        /// </summary>
        public const string Xml = "application/xml";
    }
}
