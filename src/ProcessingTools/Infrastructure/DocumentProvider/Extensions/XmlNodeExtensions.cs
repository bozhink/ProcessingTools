namespace ProcessingTools.DocumentProvider.Extensions
{
    using System;
    using System.Xml;
    using ProcessingTools.Constants.Schema;

    public static class XmlNodeExtensions
    {
        public static XmlNamespaceManager GetTaxPubXmlNamespaceManager(this XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var document = (node is XmlDocument ? node : node.OwnerDocument) as XmlDocument;
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

        public static XmlNodeList SelectNodesWithTaxPubXmlNamespaceManager(this XmlNode node, string xpath)
        {
            if (node == null)
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
