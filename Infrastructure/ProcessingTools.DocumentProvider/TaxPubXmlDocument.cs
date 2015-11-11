﻿namespace ProcessingTools.DocumentProvider
{
    using System.Xml;

    public class TaxPubXmlDocument
    {
        private const string Xmlns = "xmlns";

        private const string TpPrefix = "tp";
        private const string TpUri = "http://www.plazi.org/taxpub";
        private const string TpNamespace = Xmlns + ":" + TpPrefix;

        private const string XlinkPrefix = "xlink";
        private const string XlinkUri = "http://www.w3.org/1999/xlink";
        private const string XlinkNamespace = Xmlns + ":" + XlinkPrefix;

        private const string XmlPrefix = "xml";
        private const string XmlUri = "http://www.w3.org/XML/1998/namespace";
        private const string XmlNamespace = Xmlns + ":" + XmlPrefix;

        private const string XsiPrefix = "xsi";
        private const string XsiUri = "http://www.w3.org/2001/XMLSchema-instance";
        private const string XsiNamespace = Xmlns + ":" + XsiPrefix;

        private const string MmlPrefix = "mml";
        private const string MmlUri = "http://www.w3.org/1998/Math/MathML";
        private const string MmlNamespace = Xmlns + ":" + MmlPrefix;

        private static XmlNamespaceManager namespaceManager = null;

        public TaxPubXmlDocument()
        {
            this.NameTable = new NameTable();
            this.NamespaceManager = new XmlNamespaceManager(this.NameTable);
            this.NamespaceManager.AddNamespace(TpPrefix, TpUri);
            this.NamespaceManager.AddNamespace(XlinkPrefix, XlinkUri);
            this.NamespaceManager.AddNamespace(XmlPrefix, XmlUri);
            this.NamespaceManager.AddNamespace(XsiPrefix, XsiUri);
            this.NamespaceManager.AddNamespace(MmlPrefix, MmlUri);
            this.NamespaceManager.PushScope();

            this.XmlDocument = new XmlDocument(NameTable)
            {
                PreserveWhitespace = true
            };
        }

        public TaxPubXmlDocument(string xml)
            : this()
        {
            this.XmlDocument.LoadXml(xml);

            this.XmlDocument.DocumentElement.SetAttribute(
                TpNamespace,
                this.NamespaceManager.LookupNamespace(TpPrefix));

            this.XmlDocument.DocumentElement.SetAttribute(
                XlinkNamespace,
                this.NamespaceManager.LookupNamespace(XlinkPrefix));

            this.XmlDocument.DocumentElement.SetAttribute(
                XmlNamespace,
                this.NamespaceManager.LookupNamespace(XmlPrefix));

            this.XmlDocument.DocumentElement.SetAttribute(
                XsiNamespace,
                this.NamespaceManager.LookupNamespace(XsiPrefix));

            this.XmlDocument.DocumentElement.SetAttribute(
                MmlNamespace,
                this.NamespaceManager.LookupNamespace(MmlPrefix));
        }

        public TaxPubXmlDocument(XmlDocument xml)
            : this()
        {
            this.XmlDocument.LoadXml(xml.OuterXml);
        }

        public NameTable NameTable { get; private set; }

        public XmlNamespaceManager NamespaceManager { get; private set; }

        public XmlDocument XmlDocument { get; set; }

        public static XmlDocument Create()
        {
            return new TaxPubXmlDocument().XmlDocument;
        }

        public static XmlNamespaceManager NamespceManager()
        {
            object syncLock = new object();
            if (namespaceManager == null)
            {
                lock (syncLock)
                {
                    if (namespaceManager == null)
                    {
                        namespaceManager = new TaxPubXmlDocument().NamespaceManager;
                    }
                }
            }

            return namespaceManager;
        }
    }
}