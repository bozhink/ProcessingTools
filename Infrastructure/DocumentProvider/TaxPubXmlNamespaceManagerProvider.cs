namespace ProcessingTools.DocumentProvider
{
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Nlm.Publishing.Constants;

    public class TaxPubXmlNamespaceManagerProvider : IXmlNamespaceManagerProvider
    {
        private readonly static XmlNamespaceManager NamespaceManager;

        static TaxPubXmlNamespaceManagerProvider()
        {
            var nameTable = new NameTable();
            NamespaceManager = new XmlNamespaceManager(nameTable);
            NamespaceManager.AddNamespace(Namespaces.TaxPubNamespacePrefix, Namespaces.TaxPubNamespaceUri);
            NamespaceManager.AddNamespace(Namespaces.XlinkNamespacePrefix, Namespaces.XlinkNamespaceUri);
            NamespaceManager.AddNamespace(Namespaces.XmlNamespacePrefix, Namespaces.XmlNamespaceUri);
            NamespaceManager.AddNamespace(Namespaces.XsiNamespacePrefix, Namespaces.XsiNamespaceUri);
            NamespaceManager.AddNamespace(Namespaces.MathMLNamespacePrefix, Namespaces.MathMLNamespaceUri);
            NamespaceManager.PushScope();
        }

        public XmlNamespaceManager Create() => NamespaceManager;
    }
}
