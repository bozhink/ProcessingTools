using System;
using System.Xml;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

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

		protected void NormalizeXmlToSystemXml()
		{
			xml = Base.NormalizeNlmToSystemXml(config, xml);
		}

		protected void NormalizeSystemXmlToCurrent()
		{
			if (config.NlmStyle)
			{
				xml = Base.NormalizeSystemToNlmXml(config, xml);
			}
		}

		protected void ParseXmlDocumentToXmlString()
		{
			xml = xmlDocument.OuterXml;
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

		public static List<string> GetStringListOfUniqueXmlNodes(XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
		{
			List<string> result = new List<string>();
			try
			{
				XmlNodeList nodeList;
				if (namespaceManager == null)
				{
					nodeList = xml.SelectNodes(xpath);
				}
				else
				{
					nodeList = xml.SelectNodes(xpath, namespaceManager);
				}

				result = Base.GetStringListOfUniqueXmlNodes(nodeList);
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, 0, 1);
			}

			return result;
		}

		public static List<string> GetStringListOfUniqueXmlNodes(IEnumerable xmlNodeList)
		{
			List<string> result = new List<string>();
			try
			{
				result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, 0, 1);
			}

			return result;
		}

		public static string NormalizeNlmToSystemXml(Config config, string xml)
		{
			return XsltOnString.ApplyTransform(config.formatXslNlmToSystem, xml);
		}

		public static string NormalizeNlmToSystemXml(Config config, XmlDocument xml)
		{
			return XsltOnString.ApplyTransform(config.formatXslNlmToSystem, xml);
		}

		public static string NormalizeSystemToNlmXml(Config config, string xml)
		{
			return XsltOnString.ApplyTransform(config.formatXslSystemToNlm, xml);
		}

		public static string NormalizeSystemToNlmXml(Config config, XmlDocument xml)
		{
			return XsltOnString.ApplyTransform(config.formatXslSystemToNlm, xml);
		}
	}
}
