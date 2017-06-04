namespace ProcessingTools.DocumentProvider
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Contracts;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;

    public class TaxPubDocument : ITaxPubDocument
    {
        private const string Xmlns = "xmlns";
        private Encoding encoding;

        public TaxPubDocument(Encoding encoding)
        {
            this.NameTable = new NameTable();
            this.NamespaceManager = new XmlNamespaceManager(this.NameTable);
            this.NamespaceManager.AddNamespace(Namespaces.TaxPubNamespacePrefix, Namespaces.TaxPubNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XlinkNamespacePrefix, Namespaces.XlinkNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XmlNamespacePrefix, Namespaces.XmlNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.XsiNamespacePrefix, Namespaces.XsiNamespaceUri);
            this.NamespaceManager.AddNamespace(Namespaces.MathMLNamespacePrefix, Namespaces.MathMLNamespaceUri);
            this.NamespaceManager.PushScope();

            this.XmlDocument = new XmlDocument(this.NameTable)
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
                throw new ArgumentNullException(nameof(xml));
            }

            this.Xml = xml.OuterXml;
        }

        public TaxPubDocument(XmlDocument xml)
            : this(xml, new UTF8Encoding())
        {
        }

        public Encoding Encoding
        {
            get
            {
                return this.encoding;
            }

            private set
            {
                this.encoding = value ?? throw new ArgumentNullException(nameof(this.Encoding));
            }
        }

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
                    throw new ArgumentNullException(nameof(this.Xml));
                }

                this.XmlDocument.LoadXml(value);

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.TaxPubNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.TaxPubNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XlinkNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XlinkNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XmlNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XmlNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.XsiNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.XsiNamespacePrefix));

                this.XmlDocument.DocumentElement.SetAttribute(
                    Xmlns + ":" + Namespaces.MathMLNamespacePrefix,
                    this.NamespaceManager.LookupNamespace(Namespaces.MathMLNamespacePrefix));
            }
        }

        public XmlDocument XmlDocument { get; private set; }

        public SchemaType SchemaType { get; set; }

        public IQueryable<XmlNode> SelectNodes(string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var query = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager)
                .Cast<XmlNode>()
                .AsQueryable();

            return query;
        }

        public XmlNode SelectSingleNode(string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            var node = this.XmlDocument.SelectSingleNode(xpath, this.NamespaceManager);

            return node;
        }
    }
}
