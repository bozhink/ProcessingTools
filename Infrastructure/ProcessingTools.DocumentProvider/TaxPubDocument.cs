namespace ProcessingTools.DocumentProvider
{
    using System;
    using System.Text;
    using System.Xml;
    using Contracts;

    public class TaxPubDocument : IDocument
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

        public TaxPubDocument(Encoding encoding)
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

            this.Encoding = encoding;
        }

        public TaxPubDocument()
            : this(new UTF8Encoding())
        {
        }

        public TaxPubDocument(string xml, Encoding encoding)
            : this(encoding)
        {
            this.Xml = xml;
        }

        public TaxPubDocument(string xml)
            : this(xml, new UTF8Encoding())
        {
        }

        public TaxPubDocument(XmlDocument xml, Encoding encoding)
            : this(encoding)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            this.Xml = xml.OuterXml;
        }

        public TaxPubDocument(XmlDocument xml)
            : this(xml, new UTF8Encoding())
        {
        }

        public Encoding Encoding { get; private set; }

        public NameTable NameTable { get; private set; }

        public XmlNamespaceManager NamespaceManager { get; private set; }

        public string Xml
        {
            get
            {
                return this.XmlDocument.OuterXml;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value", "XmlDocument string is null or whitespace.");
                }

                this.XmlDocument.LoadXml(value);

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
        }

        public XmlDocument XmlDocument { get; private set; }

        public static XmlNamespaceManager NamespceManager()
        {
            object syncLock = new object();
            if (namespaceManager == null)
            {
                lock (syncLock)
                {
                    if (namespaceManager == null)
                    {
                        namespaceManager = new TaxPubDocument().NamespaceManager;
                    }
                }
            }

            return namespaceManager;
        }
    }
}