using System;
using System.Xml;
using System.Text;

namespace Base
{
    abstract public class Base
    {
        protected string xml;
        protected XmlDocument xmlDocument;
        protected Config config;
        protected XmlNamespaceManager namespaceManager;
        protected UTF8Encoding encoding;

        public Base()
        {
            this.xml = string.Empty;
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            namespaceManager = Base.TaxPubNamespceManager(xmlDocument);
            encoding = new UTF8Encoding(false);
        }

        public Base(string xml)
        {
            this.xml = xml;
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            try
            {
                this.xmlDocument.LoadXml(xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
            }
            namespaceManager = Base.TaxPubNamespceManager(xmlDocument);
            encoding = new UTF8Encoding(false);
        }

        public Base(Config config)
        {
            this.config = config;
            this.xml = string.Empty;
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            namespaceManager = Base.TaxPubNamespceManager(xmlDocument);
            encoding = new UTF8Encoding(false);
        }

        protected void ParseXmlStringToXmlDocument()
        {
            try
            {
                this.xmlDocument.LoadXml(xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 10, 3);
            }
        }

        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        public XmlDocument XmlDocument
        {
            get { return xmlDocument; }
            set { xmlDocument = value; }
        }

        public Config Config
        {
            get { return config; }
            set { config = value; }
        }

        public static XmlNamespaceManager TaxPubNamespceManager()
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(new XmlDocument().NameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlNameTable nameTable)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(nameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlDocument xmlDocument)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(xmlDocument.NameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }
    }
}
