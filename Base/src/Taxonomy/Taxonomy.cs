using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Base.Taxonomy
{
	public static class Taxonomy
	{
		/*
		 * Messages
		 */
		public static bool EmptyGenus(string source, Species sp)
		{
			if (String.Compare(sp.genus, string.Empty) == 0)
			{
				Console.WriteLine("\nERROR: Empty genus name found!:\n{0}\n", source);
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void PrintNextShortened(Species sp)
		{
			Console.WriteLine("\nNext shortened taxon:\t{0}", sp.speciesName);
		}

		public static void PrintSubstitutionMessage(Species original, Species substitution)
		{
			Console.WriteLine("\tSubstitution:\t{0}\t-->\t{1}", original.speciesName, substitution.speciesName);
		}

		public static void PrintSubstitutionMessage1(Species original, Species substitution)
		{
			Console.WriteLine("\tSubstitution:\t{0}\t-->\t{1}", original.speciesName, substitution.speciesNameGenusSubgenus);
		}

		public static void PrintSubstitutionMessageFail(Species original, Species substitution)
		{
			Console.WriteLine("\tFailed Subst:\t{0}\t<->\t{1}", original.speciesName, substitution.speciesName);
		}

		public static void PrintMethodMessage(string name)
		{
			Console.WriteLine("\n\n#\n##\n### {0} will be executed...\n##\n#\n", name);
		}

		public static void PrintFoundMessage(string where, Species sp)
		{
			Console.WriteLine("....... Found in {0}: {1}", where, sp.speciesName);
		}

		/*
		 * Taxa lists
		 */
		public static List<string> ListOfShortenedTaxa(XmlNode xml)
		{
			//string xpath = "//tp:taxon-name[@type='lower'][tp:taxon-name-part[@full-name[normalize-space(.)='']]][tp:taxon-name-part[@taxon-name-part-type='genus']][normalize-space(tp:taxon-name-part[@taxon-name-part-type='species'])!='']";
			string xpath = "//tn[@type='lower'][tn-part[@full-name[normalize-space(.)='']][normalize-space(.)!='']][tn-part[@type='genus']][normalize-space(tn-part[@type='species'])!='']";
			return Base.GetStringListOfUniqueXmlNodes(xml, xpath, Base.TaxPubNamespceManager());
		}

		public static List<string> ListOfNonShortenedTaxa(XmlNode xml)
		{
			//string xpath = "//tp:taxon-name[count(tp:taxon-name-part[normalize-space(@full-name)=''])=0][tp:taxon-name-part[@taxon-name-part-type='genus']]";
			//string xpath = "//tp:taxon-name[@type='lower'][not(tp:taxon-name-part[@full-name=''])][tp:taxon-name-part[@taxon-name-part-type='genus']]";
			string xpath = "//tn[@type='lower'][not(tn-part[@full-name=''])][tn-part[@type='genus']]";
			XmlDocument xd = new XmlDocument();
			XmlNamespaceManager nm = Base.TaxPubNamespceManager();
			XmlNodeList nodeList = xml.SelectNodes(xpath, nm);
			List<XmlNode> newList = new List<XmlNode>();
			foreach (XmlNode node in nodeList)
			{
				XmlNode taxonName = xd.CreateElement("tn");
				foreach (XmlNode innerNode in node.SelectNodes(".//*", nm))
				{
					XmlNode newNode = xd.CreateElement("tn-part");
					foreach (XmlAttribute attribute in innerNode.Attributes)
					{
						if (attribute.Name.Contains("type"))
						{
							XmlAttribute newAttribute = xd.CreateAttribute(attribute.Name);
							newAttribute.InnerText = attribute.InnerText;
							newNode.Attributes.Append(newAttribute);
						}
					}
					if (innerNode.Attributes["full-name"] != null)
					{
						newNode.InnerText = innerNode.Attributes["full-name"].InnerText;
					}
					else
					{
						newNode.InnerText = innerNode.InnerText;
					}
					taxonName.AppendChild(newNode);
				}
				newList.Add(taxonName);
			}
			return Base.GetStringListOfUniqueXmlNodes(newList);
		}

		public static List<string> ExtractTaxa(XmlNode xml, bool stripTags = false, TaxaType type = TaxaType.Any)
		{
			List<string> result = new List<string>();
			string xpath = string.Empty;
			string typeString = type.ToString().ToLower();
			switch (type)
			{
				case TaxaType.Lower:
				case TaxaType.Higher:
					xpath = string.Format("//tn[@type='{0}']|//tp:taxon-name[@type='{0}']", typeString);
					break;
				case TaxaType.Any:
					xpath = "//tn|//tp:taxon-name";
					break;
			}

			if (xpath != string.Empty)
			{
				XmlNodeList nodeList = xml.SelectNodes(xpath, Base.TaxPubNamespceManager());
				if (stripTags)
				{
					result = nodeList.Cast<XmlNode>().Select(c =>
						Regex.Replace(
							Regex.Replace(
								Regex.Replace(c.InnerXml, @"</[^>]*>(?=\S)(?!\Z)", " "),
								@"<[^>]+ full-name=""([^<>""]+)""[^>]*>\S*", "$1"),
							@"<[^>]+>", "")
						).Distinct().ToList();
				}
				else
				{
					result = Base.GetStringListOfUniqueXmlNodes(nodeList);
				}

				result.Sort();
			}

			return result;
		}

		public static string ExtractTaxa(Config config, string xml)
		{
			return XsltOnString.ApplyTransform(config.floraExtractTaxaXslPath, xml);
		}

		public static string DistinctTaxa(Config config, string xml)
		{
			return XsltOnString.ApplyTransform(config.floraDistrinctTaxaXslPath, xml);
		}

		public static string GenerateTagTemplate(Config config, string xml)
		{
			return XsltOnString.ApplyTransform(config.floraGenerateTemplatesXslPath, xml);
		}
	}

	public enum TaxaType
	{
		Lower,
		Higher,
		Any
	}
}
