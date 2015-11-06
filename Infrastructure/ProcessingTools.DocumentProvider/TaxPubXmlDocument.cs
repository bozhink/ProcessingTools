namespace ProcessingTools.DocumentProvider
{
    using System.Xml;

    public class TaxPubXmlDocument
    {
        private static XmlNamespaceManager namespaceManager = null;

        public static XmlNamespaceManager NamespceManager()
        {
            object syncLock = new object();
            if (namespaceManager == null)
            {
                lock (syncLock)
                {
                    if (namespaceManager == null)
                    {
                        namespaceManager = NamespceManager(new XmlDocument().NameTable);
                    }
                }
            }

            return namespaceManager;
        }

        public static XmlNamespaceManager NamespceManager(XmlDocument xmlDocument)
        {
            return NamespceManager(xmlDocument.NameTable);
        }

        public static XmlNamespaceManager NamespceManager(XmlNameTable nameTable)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(nameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }
    }
}