// <copyright file="XmlNodeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Extensions
{
    using System;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// <see cref="XmlNode"/> Extensions.
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Gets <see cref="XmlNamespaceManager"/> for TaxPub namespaces.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> object, which Owner Document will be used.</param>
        /// <returns><see cref="XmlNamespaceManager"/> for TaxPub namespaces.</returns>
        public static XmlNamespaceManager GetTaxPubXmlNamespaceManager(this XmlNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = node.OwnerDocument;
            var nameTable = document.NameTable;
            var namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace(Namespaces.TaxPubNamespacePrefix, Namespaces.TaxPubNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XlinkNamespacePrefix, Namespaces.XlinkNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XmlNamespacePrefix, Namespaces.XmlNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.XsiNamespacePrefix, Namespaces.XsiNamespaceUri);
            namespaceManager.AddNamespace(Namespaces.MathMLNamespacePrefix, Namespaces.MathMLNamespaceUri);
            namespaceManager.PushScope();

            return namespaceManager;
        }

        /// <summary>
        /// Select nodes with TaxPub <see cref="XmlNamespaceManager"/>.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> to be requested.</param>
        /// <param name="xpath">XPath string to be evaluated.</param>
        /// <returns><see cref="XmlNodeList"/> object.</returns>
        public static XmlNodeList SelectNodesWithTaxPubXmlNamespaceManager(this XmlNode node, string xpath)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var namespaceManager = node.GetTaxPubXmlNamespaceManager();
            var nodeList = node.SelectNodes(xpath, namespaceManager);

            return nodeList;
        }
    }
}
