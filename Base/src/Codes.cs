using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
	public class Codes : Base
	{
		private const string selectNodesToTagAbbreviationsXPathTemplate = "//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),'{0}')][count(.//node()[contains(string(.),'{0}')])=0]";
		private const string abbreviationReplaceTagName = "abbreviationReplaceTagName";

		public Codes()
			: base()
		{
		}

		public Codes(string xml)
			: base(xml)
		{
		}

		public void TagAbbreviationsInText()
		{
			ParseXmlStringToXmlDocument();

			// Do not change this sequence
			TagAbbreviationsInSpecificNode("//graphic|//media|//disp-formula-group");
			TagAbbreviationsInSpecificNode("//chem-struct-wrap|//fig|//supplementary-material|//table-wrap");
			TagAbbreviationsInSpecificNode("//fig-group|//table-wrap-group");
			TagAbbreviationsInSpecificNode("//boxed-text");
			TagAbbreviationsInSpecificNode("/");

			xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, "</?" + abbreviationReplaceTagName + "[^>]*>", "");
			xml = xmlDocument.OuterXml;
		}

		private void TagAbbreviationsInSpecificNode(string selectSpecificNodeXPath)
		{
			XmlNodeList specificNodes = xmlDocument.SelectNodes(selectSpecificNodeXPath, namespaceManager);
			foreach (XmlNode specificNode in specificNodes)
			{
				List<Abbreviation> abbreviationsList = specificNode.SelectNodes(".//abbrev", namespaceManager)
					.Cast<XmlNode>().Select(a => ConvertAbbrevXmlNodeToAbbreviation(a)).ToList();

				foreach (Abbreviation abbreviation in abbreviationsList)
				{
					string xpath = string.Format(selectNodesToTagAbbreviationsXPathTemplate, abbreviation.content);
					foreach (XmlNode nodeInspecificNode in specificNode.SelectNodes(xpath, namespaceManager))
					{
						bool doReplace = false;
						if (nodeInspecificNode.InnerXml == string.Empty)
						{
							if (nodeInspecificNode.OuterXml.IndexOf("<!--") == 0)
							{
								// This node is a comment. Do not replace matches here.
								doReplace = false;
							}
							else if (nodeInspecificNode.OuterXml.IndexOf("<?") == 0)
							{
								// This node is a processing instruction. Do not replace matches here.
								doReplace = false;
							}
							else if (nodeInspecificNode.OuterXml.IndexOf("<!DOCTYPE") == 0)
							{
								// This node is a DOCTYPE node. Do not replace matches here.
								doReplace = false;
							}
							else if (nodeInspecificNode.OuterXml.IndexOf("<![CDATA[") == 0)
							{
								// This node is a CDATA node. Do nothing?
								doReplace = false;
							}
							else
							{
								// This node is a text node. Tag this tex¾t and replace in InnerXml
								doReplace = true;
							}
						}
						else
						{
							// This is a named node
							doReplace = true;
						}

						if (doReplace)
						{
							XmlElement newNode = xmlDocument.CreateElement("abbreviationReplaceTagName");
							newNode.InnerXml = Regex.Replace(nodeInspecificNode.OuterXml, abbreviation.searchPattern, abbreviation.replacePattern);
							nodeInspecificNode.ParentNode.ReplaceChild(newNode, nodeInspecificNode);
						}
					}
				}
			}
		}

		public Abbreviation ConvertAbbrevXmlNodeToAbbreviation(XmlNode abbrev)
		{
			Abbreviation abbreviation = new Abbreviation();

			abbreviation.content = Regex.Replace(
						Regex.Replace(
							Regex.Replace(abbrev.InnerXml, @"<def.+</def>", ""),
							@"<def[*>]</def>|</?b[^>]*>", ""),
						@"\A\W+|\W+\Z", "");

			if (abbrev.Attributes["content-type"] != null)
			{
				abbreviation.contentType = abbrev.Attributes["content-type"].InnerText;
			}

			if (abbrev["def"] != null)
			{
				abbreviation.definition = Regex.Replace(
					Regex.Replace(abbrev["def"].InnerXml, "<[^>]*>", ""),
					@"\A[=,;:\s–—−-]|[=,;:\s–—−-]\Z|\s+(?=\s)", "");
			}

			return abbreviation;
		}

		public struct Abbreviation
		{
			public string content;

			public string contentType;

			public string definition;

			public string searchPattern
			{
				get
				{
					return "\\b(" + this.content + ")\\b";
				}
			}

			public string replacePattern
			{
				get
				{
					return "<abbrev" +
						((this.contentType == null || this.contentType == string.Empty) ? "" : @" content-type=""" + this.contentType + @"""") +
						((this.definition == null || this.definition == string.Empty) ? "" : @" xlink:title=""" + this.definition + @"""") +
						@" xmlns:xlink=""http://www.w3.org/1999/xlink""" +
						">$1</abbrev>";
				}
			}
		}

		public void TagQuantities()
		{
			string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

			ParseXmlStringToXmlDocument();
			foreach (XmlNode node in xmlDocument.SelectNodes(xpath,namespaceManager))
			{
				string replace = node.InnerXml;

				// 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
				string pattern = @"((?:(?:[–—−‒-]\s*)?\d+(?:[,\.]\d+)?(?:\s*[×\*])?\s*)+(?:[kdcmµ]m|[º°˚]\s*C|bp|ft|m|[kdcmµ]M|[kdcmµ]mol|mile|min(?:ute))\b)";
				Match m = Regex.Match(replace, pattern);
				if (m.Success)
				{
					Alert.Message(m.Value);
					replace = Regex.Replace(replace, pattern, "<quantity>$1</quantity>");
					node.InnerXml = replace;
				}
			}

			ParseXmlDocumentToXmlString();
		}

		public void TagDates()
		{
			string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

			ParseXmlStringToXmlDocument();
			foreach (XmlNode node in xmlDocument.SelectNodes(xpath, namespaceManager))
			{
				string replace = node.InnerXml;

				// 18 Jan 2008
				string pattern = @"((?:(?:(?:[1-2][0-9]|3[0-1]|0?[1-9])(?:\s*[–—−‒-]\s*)?)+\W{0,4})?(?:Jan(?:uary)?|Febr?(?:uary)?|Mar(?:ch)?|Apr(?:il)?|May|June?|July?|Aug(?:ust)?|Sept?(?:ember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)\W{0,4}(?:1[6-9][0-9]|20[0-9])[0-9](?![0-9]))";
				Match m = Regex.Match(replace, pattern);
				if (m.Success)
				{
					Alert.Message(m.Value);
					replace = Regex.Replace(replace, pattern, "<date>$1</date>");
					node.InnerXml = replace;
				}
			}

			ParseXmlDocumentToXmlString();
		}

		public void SelectCodes()
		{
			const string codePattern = @"\b[A-Z0-9](\s?[\.:\\\/–—−-]?\s?[A-Z0-9]\s?)+[A-Z0-9]\b";

			List<string> codeWords = new List<string>();
			XmlDocument cleanedXmlDocument = new XmlDocument();

			ParseXmlStringToXmlDocument();
			cleanedXmlDocument = xmlDocument;

			cleanedXmlDocument.InnerXml = Regex.Replace(cleanedXmlDocument.InnerXml, @"(?<=</xref>)\s*:\s*" + codePattern, "");

			cleanedXmlDocument.LoadXml(XsltOnString.ApplyTransform(config.codesRemoveNonCodeNodes, cleanedXmlDocument));

			for (Match m = Regex.Match(cleanedXmlDocument.InnerText, codePattern); m.Success; m = m.NextMatch())
			{
				codeWords.Add(m.Value);
			}

			codeWords = codeWords.Distinct().ToList();
			codeWords.Sort();
			Alert.Message("\n\n" + codeWords.Count + " code words in article\n");
			foreach(string word in codeWords)
			{
				Alert.Message(word);
			}
		}
	}
}
