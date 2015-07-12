using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading.Tasks;

namespace Base
{
	public class Flora : Base
	{
		public Flora()
			: base()
		{
		}

		public Flora(string xml)
			: base(xml)
		{
		}

		public void ExtractTaxa()
		{
			xml = XsltOnString.ApplyTransform(config.floraExtractTaxaXslPath, xml);
		}

		public string ExtractTaxaParts()
		{
			return XsltOnString.ApplyTransform(config.floraExtractTaxaPartsXslPath, xml);
		}

		public void DistinctTaxa()
		{
			xml = XsltOnString.ApplyTransform(config.floraDistrinctTaxaXslPath, xml);
		}

		public static string DistinctTaxa(string xml)
		{
			return XsltOnString.ApplyTransform(@"C:\bin\taxa.distinct.xslt", xml);
		}

		public void GenerateTagTemplate()
		{
			XmlDocument generatedTemplate = new XmlDocument();
			generatedTemplate.LoadXml(Flora.DistinctTaxa(XsltOnString.ApplyTransform(config.floraGenerateTemplatesXslPath, xml)));
			generatedTemplate.Save(config.floraTemplatesOutputXmlPath);
		}

		public void PerformReplace()
		{
			ParseXmlStringToXmlDocument();

			XmlDocument template = new XmlDocument();
			template.Load(config.floraTemplatesOutputXmlPath);

			XmlNode root = template.DocumentElement;
			Alert.Message(root.ChildNodes.Count);

			xml = xmlDocument.OuterXml;
			for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
			{
				XmlNode taxon = root.ChildNodes.Item(i);
				XmlNode find = taxon.FirstChild;
				XmlNode replace = taxon.LastChild;

				string pattern = Regex.Replace(Regex.Escape(find.InnerXml), @"(\W)\\ ", "$1?\\s*");
				pattern = Regex.Replace(pattern, "\\s+", "\\b\\s*\\b");

				xml = Regex.Replace(xml,
					"(?<![a-z-])(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
					"<tn>$1</tn>");
			}

			string infraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
			string lowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

			xml = Regex.Replace(xml, infraspecificPattern + "\\s*<tn>", "<tn>$1 ");
			xml = Regex.Replace(xml, "(?<!<tn>)(" + infraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

			xml = Regex.Replace(xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
			xml = Regex.Replace(xml, "(<tn>)" + infraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

			xml = Regex.Replace(xml, "</tn>\\s*<tn>" + infraspecificPattern, " $1");

			// TODO: Here we must remove tn/tn
			{
				ParseXmlStringToXmlDocument();
				XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
				foreach (XmlNode node in nodeList)
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
				}
				xml = xmlDocument.OuterXml;
			}

			// Guess new taxa:
			for (int i = 0; i < 10; i++)
			{
				xml = Regex.Replace(xml,
					"(</tn>,?(\\s+and)?\\s+)(" + infraspecificPattern + "?" + lowerPattern + ")",
					"$1<tn>$3</tn>");
			}

			// Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
			xml = Regex.Replace(xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

			xml = Regex.Replace(xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + infraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

			xml = Regex.Replace(xml,
				"(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + infraspecificPattern + ")?" + lowerPattern + ")",
				"<tn>$1</tn>");

			// TODO: Here we must remove tn/tn
			{
				ParseXmlStringToXmlDocument();
				XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
				foreach (XmlNode node in nodeList)
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
				}
				xml = xmlDocument.OuterXml;
			}

			// Remove taxa in toTaxon
			{
				ParseXmlStringToXmlDocument();
				XmlNodeList nodeList = xmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
				foreach (XmlNode node in nodeList)
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
				}
				xml = xmlDocument.OuterXml;
			}
		}

		public void ParseInfra()
		{
			ParseXmlStringToXmlDocument();
			XmlNodeList nodeList = xmlDocument.SelectNodes("//tn");
			foreach (XmlNode node in nodeList)
			{
				node.InnerXml = Regex.Replace(node.InnerXml,
					@"([Vv]ar\.)\s*([a-z\.-]+)",
					"<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"variety\">$2</tn-part>");

				node.InnerXml = Regex.Replace(node.InnerXml,
					@"([Ff]orma)\s*([a-z\.-]+)",
					"<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"forma\">$2</tn-part>");

				node.InnerXml = Regex.Replace(node.InnerXml,
					@"([Ss]ub\s*sp\.|[Ss]sp\.|[Ss]pp\.)\s*([a-z\.-]+)",
					"<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

				node.InnerXml = Regex.Replace(node.InnerXml,
					@"([Ss]ect\.|[Ss]ection)\s*([A-Z]?[a-z\.-]+)",
					"<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"section\">$2</tn-part>");

				node.InnerXml = Regex.Replace(node.InnerXml,
					@"([Ss]ub\s*sect\.|[Ss]ub\s*section)\s*([A-Z]?[a-z\.-]+)",
					"<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subsection\">$2</tn-part>");
			}
			xml = xmlDocument.OuterXml;
			xml = Regex.Replace(xml, "(?<=</tn-part>)(?=<tn)", " ");
		}

		public void ParseTn()
		{
			ParseXmlStringToXmlDocument();
			XmlDocument template = new XmlDocument();
			template.Load(config.floraTemplatesOutputXmlPath);

			XmlNode root = template.DocumentElement;

			// Get only full-named taxa
			XmlNodeList templateList = root.SelectNodes("//taxon[count(replace/tn/tn-part[normalize-space(.)=''])=0]");
			Alert.Message(templateList.Count);

			XmlNodeList nodeList = xmlDocument.SelectNodes("//tn");
			Alert.Message(nodeList.Count);

			Parallel.For(0, nodeList.Count, index =>
			{
				XmlNode node = nodeList.Item(index);
				for (int i = templateList.Count - 1; i >= 0; i--)
				{
					XmlNode taxon = templateList.Item(i);
					XmlNode find = taxon.FirstChild;
					XmlNode replace = taxon.LastChild.FirstChild;

					if (find.InnerText.Length > 2)
					{
						string pattern = Regex.Replace(Regex.Escape(find.InnerText), @"([^\w\.])\\ ", "$1?\\s*");
						pattern = Regex.Replace(pattern, "\\s+", "\\b\\s*\\b");
						pattern = "(?<!\">)(?<!=\")" + pattern + "(?!\")(?!</tn-part)";

						if (Regex.Match(node.InnerXml, pattern).Success)
						{
							node.InnerXml = Regex.Replace(node.InnerXml, pattern, replace.InnerXml);
						}
					}
				}
			}
			);

			xml = xmlDocument.OuterXml;
			xml = Regex.Replace(xml, "(?<=</tn-part>)(?=<tn)", " ");
		}
	}
}
