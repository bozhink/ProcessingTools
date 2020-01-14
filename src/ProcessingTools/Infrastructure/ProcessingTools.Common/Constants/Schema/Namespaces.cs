// <copyright file="Namespaces.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Schema
{
    /// <summary>
    /// XML namespaces.
    /// </summary>
    public static class Namespaces
    {
#pragma warning disable S1075 // URIs should not be hardcoded

        /// <summary>
        /// Abbreviations namespace.
        /// </summary>
        public const string AbbreviationsNamespace = "urn:processing-tools-abbreviations";

        /// <summary>
        /// External links namespace.
        /// </summary>
        public const string ExternalLinksNamespace = "urn:processing-tools-external-links";

        /// <summary>
        /// TaxPub namespace prefix.
        /// </summary>
        public const string TaxPubNamespacePrefix = "tp";

        /// <summary>
        /// TaxPub namespace URI.
        /// </summary>
        public const string TaxPubNamespaceUri = "http://www.plazi.org/taxpub";

        /// <summary>
        /// Xlink namespace prefix.
        /// </summary>
        public const string XlinkNamespacePrefix = "xlink";

        /// <summary>
        /// Xlink namespace URI.
        /// </summary>
        public const string XlinkNamespaceUri = "http://www.w3.org/1999/xlink";

        /// <summary>
        /// XML namespace prefix.
        /// </summary>
        public const string XmlNamespacePrefix = "xml";

        /// <summary>
        /// XML namespace URI.
        /// </summary>
        public const string XmlNamespaceUri = "http://www.w3.org/XML/1998/namespace";

        /// <summary>
        /// Xsi namespace prefix.
        /// </summary>
        public const string XsiNamespacePrefix = "xsi";

        /// <summary>
        /// Xsi namespace uri.
        /// </summary>
        public const string XsiNamespaceUri = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// MathML namespace prefix.
        /// </summary>
        public const string MathMLNamespacePrefix = "mml";

        /// <summary>
        /// MathML namespace URI.
        /// </summary>
        public const string MathMLNamespaceUri = "http://www.w3.org/1998/Math/MathML";

#pragma warning restore S1075 // URIs should not be hardcoded
    }
}
