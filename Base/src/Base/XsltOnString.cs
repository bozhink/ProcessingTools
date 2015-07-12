using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Base
{
	public class XsltOnString
	{
		private string xslFileName;
		private string xml;

		public XsltOnString()
		{
			xslFileName = string.Empty;
		}

		public XsltOnString(string xslFile)
		{
			xslFileName = xslFile;
		}

		public XsltOnString(string xslFile, string xmlString)
		{
			xslFileName = xslFile;
			xml = xmlString;
		}

		public string XslFileName
		{
			get { return xslFileName; }
			set { xslFileName = value; }
		}

		public string Xml
		{
			get { return xml; }
			set { xml = value; }
		}

		public void ApplyTransform()
		{
			if (xslFileName == string.Empty || xml == string.Empty)
			{
				Alert.Message("ApplyTransofm: ERROR: the xml string is emplty!");
				return;
			}

			XslCompiledTransform xslTransform = new XslCompiledTransform();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			MemoryStream stream = new MemoryStream();
			try
			{
				xslTransform.Load(xslFileName);
				xmlDocument.LoadXml(xml);
				xslTransform.Transform(xmlDocument, null, stream);
				stream.Position = 0;
				StreamReader streamReader = new StreamReader(stream);
				xml = streamReader.ReadToEnd();
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 100, "XSLT file: " + xslFileName);
			}
		}

		public static string ApplyTransform(string xslFileName, string xml)
		{
			string result = string.Empty;
			if (xslFileName == String.Empty || xml == String.Empty)
			{
				Alert.Message("ApplyTransofm: ERROR: the xml string is emplty!");
				return result;
			}

			XslCompiledTransform xslTransform = new XslCompiledTransform();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			MemoryStream stream = new MemoryStream();
			StreamReader streamReader = null;
			try
			{
				xslTransform.Load(xslFileName);
				xmlDocument.LoadXml(xml);
				xslTransform.Transform(xmlDocument, null, stream);
				stream.Position = 0;
				streamReader = new StreamReader(stream);
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, "XsltOnString", 100, "XSLT file: " + xslFileName);
			}
			finally
			{
				if (streamReader != null)
				{
					result = streamReader.ReadToEnd();
				}
			}

			return result;
		}

		public static string ApplyTransform(string xslFileName, XmlDocument xmlDocument)
		{
			string result = string.Empty;
			if (xslFileName == String.Empty)
			{
				Alert.Message("ApplyTransofm: ERROR: the xslFileName string is emplty!");
				return result;
			}

			XslCompiledTransform xslTransform = new XslCompiledTransform();
			MemoryStream stream = new MemoryStream();
			StreamReader streamReader = null;
			try
			{
				xslTransform.Load(xslFileName);
				xslTransform.Transform(xmlDocument, null, stream);
				stream.Position = 0;
				streamReader = new StreamReader(stream);
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, "XsltOnString", 100, "XSLT file: " + xslFileName);
			}
			finally
			{
				if (streamReader != null)
				{
					result = streamReader.ReadToEnd();
				}
			}

			return result;
		}

		public static string ApplyTransform(string xslFileName, XmlReader reader)
		{
			string result = string.Empty;
			if (xslFileName == String.Empty)
			{
				Alert.Message("ApplyTransofm: ERROR: the xslFileName string is emplty!");
				return result;
			}

			XslCompiledTransform xslTransform = new XslCompiledTransform();
			MemoryStream stream = new MemoryStream();
			StreamReader streamReader = null;
			try
			{
				xslTransform.Load(xslFileName);
				xslTransform.Transform(reader, null, stream);
				stream.Position = 0;
				streamReader = new StreamReader(stream);
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, "XsltOnString", 100, "XSLT file: " + xslFileName);
			}
			finally
			{
				if (streamReader != null)
				{
					result = streamReader.ReadToEnd();
				}
			}

			return result;
		}
	}
}
