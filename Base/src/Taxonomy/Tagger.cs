using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Base.Taxonomy
{
	public class Tagger : Base
	{
		public Tagger()
			: base()
		{
		}

		public Tagger(string xml)
			: base(xml)
		{
		}

		public Tagger(Config config)
			: base(config)
		{
		}

		private const string higherTaxaReplacePattern = "<tn type=\"higher\">$1</tn>";
		private const string lowerRaxaReplacePattern = "<tn type=\"lower\">$1</tn>";

		public static string TagItalics(string nodeXml, bool tagInfraspecific = false)
		{
			// Genus (Subgenus)? species subspecies?
			string replace = Regex.Replace(nodeXml, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]*)(?=</i>)", lowerRaxaReplacePattern);
			replace = Regex.Replace(replace, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", lowerRaxaReplacePattern);
			replace = Regex.Replace(replace, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]*)(?=</i>)", lowerRaxaReplacePattern);
			replace = Regex.Replace(replace, @"(?<=<i>)([A-Z][a-z\.×]+(\-[A-Z][a-z\.×]+)?\s*\(\s*[A-Za-z][a-z\.×]+\s*\)\s*[a-z\.×-]+\s*[a-z×-]+)(?=</i>)", lowerRaxaReplacePattern);
			replace = Regex.Replace(replace, @"(?<=<i>)([A-Z\.-]{3,30})(?=</i>)", lowerRaxaReplacePattern);

			replace = Regex.Replace(replace, @"‘<i>(<tn type=""lower"">)([A-Z][a-z\.×]+)(</tn>)</i>’\s*<i>([a-z\.×-]+)</i>", "$1‘$2’ $4$3");

			if (tagInfraspecific)
			{
				string infraspecificPattern;
				// Neoserica (s. l.) abnormoides, Neoserica (sensu lato) abnormis
				infraspecificPattern = @"<i(talic)?><tn type=""lower"">([^<>]*?)</tn></i(talic)?>\s*(\((?i)(\bsensu\b\s*[a-z]*|s\.?\s*[ls]\.?|s\.?\s*str\.?)\))\s*<i(talic)?>([a-z\s-]+)</i(talic)?>";
				replace = Regex.Replace(replace, infraspecificPattern,
					"<tn type=\"lower\"><basionym>$2</basionym> <sensu>$4</sensu> <specific>$7</specific></tn>");

				// Genus subgen(us)?. Subgenus sect(ion)?. Section subsect(ion)?. Subsection
				infraspecificPattern = @"<i(talic)?><tn type=""lower"">([A-Za-z\.-]+)</tn></i(talic)?>(?![,\.])(?!\s+and\b)(?!\s+as\b)(?!\s+to\b)\s*([^<>\(\)\[\]]{0,30}?)\s*(\b([Ss]ubgen(us)?|[Ss]ubg|[Ss]er|([Ss]ub)?[Ss]ect(ion)?)\b\.?)\s*(<i(talic)?>)?(<tn type=""lower"">)?([A-Za-z\.-]+(\s+[a-z\s\.-]+){0,3})(</tn>)?(</i(talic)?>)?";
				for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
				{
					replace = Regex.Replace(replace, infraspecificPattern,
						"<tn type=\"lower\"><genus>$2</genus> <genus-authority>$4</genus-authority> <infraspecific-rank>$5</infraspecific-rank> <infraspecific>$13</infraspecific></tn>");
				}
				infraspecificPattern = @"(?<=</infraspecific>)</tn>\s*([^<>]{0,100}?)\s*(\b([Ss]ubgen(us)?|[Ss]ubg|([Ss]ub)?[Ss]ect(ion)?)\b\.?)\s*(<i(talic)?>)?(<tn type=""lower"">)?([A-Za-z\.-]+(\s+[a-z\s\.-]+){0,3})(</tn>)?(</i(talic)?>)?";
				for (int i = 0; i < 3; i++)
				{
					for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
					{
						replace = Regex.Replace(replace, infraspecificPattern,
							" <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$10</infraspecific></tn>");
					}
				}
				//replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");


				// <i><tn>A. herbacea</tn></i> Walter var. <i>herbacea</i>
				// <i>Lespedeza hirta</i> (L.) Hornem. var. <i>curtissii</i>
				infraspecificPattern = @"<i(talic)?><tn type=""lower"">([^<>]*?)</tn></i(talic)?>(?![,\.])\s*(([^<>\(\)\[\]]{0,3}?\([^<>\(\)\[\]]{0,30}?\)[^<>\(\)\[\]]{0,30}?|[^<>\(\)\[\]]{0,10})?)\s*((\b([Aa]b?|[Ss]p|[Vv]ar|[Ss]ubvar|[Ss]ubvar|[Ss]ubsp|[Ss]ubspecies|[Ss]sp|f|[Ff]orma?|[Ss]t|r|[Ss]f|[Cc]f|[Nn]r|[Nn]ear|sp\. near|[Aa]ff|[Pp]rope|([Ss]ub)?[Ss]ect)\b(\.)?)|×|\?)\s*<i(talic)?>([a-z-]+)</i(talic)?>";
				for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
				{
					replace = Regex.Replace(replace, infraspecificPattern,
						"<tn type=\"lower\"><basionym>$2</basionym> <basionym-authority>$4</basionym-authority> <infraspecific-rank>$6</infraspecific-rank> <infraspecific>$12</infraspecific></tn>");
				}

				infraspecificPattern = @"(?<=</infraspecific>)</tn>\s*([^<>]{0,100}?)\s*((\b([Aa]b?|[Nn]?\.?\s*[Ss]p|[Vv]ar|[Ss]ubvar|[Ss]ubsp|[Ss]ubspecies|[Ss]sp|[Ss]ubspec|f|fo|[Ff]orma?|[Ss]t|r|[Ss]f|[Cc]f|[Nn]r|[Nn]ear|[Aa]ff|[Pp]rope|([Ss]ub)?[Ss]ect)\b(\.)?)|×)\s*<i(talic)?>([a-z-]+)</i(talic)?>";
				for (int i = 0; i < 4; i++)
				{
					for (Match m = Regex.Match(replace, infraspecificPattern); m.Success; m = m.NextMatch())
					{
						replace = Regex.Replace(replace, infraspecificPattern,
							" <authority>$1</authority> <infraspecific-rank>$2</infraspecific-rank> <infraspecific>$8</infraspecific></tn>");
					}
				}

				// Here we must extract species+subspecies in <infraspecific/>, which comes from tagging of subgenera and [sub]sections
				replace = Regex.Replace(replace, @"<infraspecific>([A-Za-z\.-]+)\s+([a-z\s\.-]+)</infraspecific>", "<infraspecific>$1</infraspecific> <species>$2</species>");

				replace = Regex.Replace(replace, " <([a-z-]+)?authority></([a-z-]+)?authority>", "");
			}
			return replace;
		}

		public void TagLowerTaxa(bool tagBasionym = false)
		{
			string xpath = string.Empty;
			if (config.NlmStyle)
			{
				if (config.TagWholeDocument)
				{
					xpath = "//*[i]";
				}
				else
				{
					xpath = "//p[.//i]|//ref[.//i]|//kwd[.//i]|//article-title[.//i]|//li[.//i]|//th[.//i]|//td[.//i]|//title[.//i]|//label[.//i]|//tp:nomenclature-citation[.//i]";
				}
			}
			else
			{
				if (config.TagWholeDocument)
				{
					xpath = "//*[i]";
				}
				else
				{
					xpath = "//p[.//i]|//li[.//i]|//td[.//i]|//th[.//i]";
				}
			}

			NormalizeXmlToSystemXml();
			ParseXmlStringToXmlDocument();

			/*
			 * The following piece of code will be executed twice: once for lower-level-content-holding tags, and next for all value tags (System)
			 */
			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes(xpath, namespaceManager))
				{
					node.InnerXml = Tagger.TagItalics(node.InnerXml, tagBasionym);
				}
				if (!config.NlmStyle && !config.TagWholeDocument)
				{
					foreach (XmlNode node in xmlDocument.SelectNodes("//value[.//i]", namespaceManager))
					{
						node.InnerXml = Tagger.TagItalics(node.InnerXml, tagBasionym);
					}
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag taxa.");
			}

			/*
			 * Put some blank spaces in taxomic names
			 */
			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes("//tn[@type='lower'][not(tn-part)]", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, @"(\.)(\w)", "$1 $2");
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Refactor tagged taxa.");
			}

			ParseXmlDocumentToXmlString();
			NormalizeSystemXmlToCurrent();
		}

		public void TagHigherTaxa()
		{
			string searchPattern = "(?<!<tn [^>]*>)(?<!name [^>]*>)(?<!<[^>]+=\"[^>]*)\\b([A-Z](?i)[a-z]*(morphae?|mida|toda|ideae|oida|genea|formes|lifera|ieae|indeae|eriae|idea|aceae|oidea|oidae|inae|ini|ina|anae|ineae|acea|oideae|mycota|mycotina|mycetes|mycetidae|phyta|phytina|opsida|phyceae|idae|phycidae|ptera|poda|phaga|itae|odea|alia|ntia|osauria))\\b(?!\"\\s?>)(?!</tn)(?!</tp:)";
			//                                                      ^^^^^^^^^^^^^^^^^ --- Do not tag attributes

			NormalizeXmlToSystemXml();
			ParseXmlStringToXmlDocument();

			/*
			 * The main part of the method
			 */
			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes("//p|//td|//th|//li", namespaceManager))
				{
					string replace = Regex.Replace(node.InnerXml, searchPattern, higherTaxaReplacePattern);
					replace = Regex.Replace(replace, "(<[^<>]*)<[^>]*>([^<>]*)</[^>]*>([^>]*>)", "$1$2$3");
					node.InnerXml = replace;
				}

				foreach (XmlNode node in xmlDocument.SelectNodes("//article-title|//title|//label", namespaceManager))
				{
					string replace = Regex.Replace(node.InnerXml, searchPattern, higherTaxaReplacePattern);
					replace = Regex.Replace(replace, "(<[^<>]*)<[^>]*>([^<>]*)</[^>]*>([^>]*>)", "$1$2$3");
					node.InnerXml = replace;
				}

				foreach (XmlNode node in xmlDocument.SelectNodes("//ref|//kwd", namespaceManager))
				{
					string replace = Regex.Replace(node.InnerXml, searchPattern, higherTaxaReplacePattern);
					replace = Regex.Replace(replace, "(<[^<>]*)<[^>]*>([^<>]*)</[^>]*>([^>]*>)", "$1$2$3");
					node.InnerXml = replace;
				}

				if (config.NlmStyle)
				{
					foreach (XmlNode node in xmlDocument.SelectNodes("//tp:nomenclature-citation", namespaceManager))
					{
						string replace = Regex.Replace(node.InnerXml, searchPattern, higherTaxaReplacePattern);
						replace = Regex.Replace(replace, "(<[^<>]*)<[^>]*>([^<>]*)</[^>]*>([^>]*>)", "$1$2$3");
						node.InnerXml = replace;
					}
				}

				foreach (XmlNode node in xmlDocument.SelectNodes("//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48']", namespaceManager))
				{
					string replace = Regex.Replace(node.InnerXml, searchPattern, higherTaxaReplacePattern);
					replace = Regex.Replace(replace, "(<[^<>]*)<[^>]*>([^<>]*)</[^>]*>([^>]*>)", "$1$2$3");
					node.InnerXml = replace;
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tagging higher taxa.");
			}

			ApplyWhiteList();

			ParseXmlDocumentToXmlString();
			NormalizeSystemXmlToCurrent();
		}

		public void UntagTaxa()
		{
			NormalizeXmlToSystemXml();
			ParseXmlStringToXmlDocument();

			try
			{
				ApplyBlackList();

				xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, @"<tn type=""higher"">([a-z]+)</tn>", "$1");

				foreach (XmlNode node in xmlDocument.SelectNodes("//tn[count(.//tn)!=0]|//a[count(.//tn)!=0]|//ext-link[count(.//tn)!=0]|//tp:treatment-meta/kwd-group/kwd/named-content[count(.//tn)!=0]|//*[@object_id='82'][count(.//tn)!=0]|//*[@id='41'][count(.//tn)!=0]|//surname[count(.//tn)!=0]|//given-names[count(.//tn)!=0]|//article/front/notes/sec", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "<tn [^>]*>|</?tn>", "");
				}

				foreach (XmlNode node in xmlDocument.SelectNodes("//*[@id='236' or @id='436' or @id='435' or @id='418' or @id='49' or @id='417' or @id='48' or @id='434' or @id='433' or @id='432' or @id='431' or @id='430' or @id='429' or @id='428' or @id='427' or @id='426' or @id='425' or @id='424' or @id='423' or @id='422' or @id='421' or @id='420' or @id='419' or @id='475' or @id='414']/value[count(.//tn)!=0]", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "<tn [^>]*>|</?tn>", "");
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			ParseXmlDocumentToXmlString();
			NormalizeSystemXmlToCurrent();
		}

		private void ApplyBlackList()
		{
			try
			{
				List<string> firstWordTaxaList = Base.GetStringListOfUniqueXmlNodes(xmlDocument, "//tn", namespaceManager)
					.Cast<string>().Select(c => Regex.Match(c, @"\w+\.?").Value).Distinct().ToList();

				XElement blackList = XElement.Load(config.blackListXmlFilePath);
				foreach (string taxon in firstWordTaxaList)
				{
					IEnumerable<string> queryResult = from item in blackList.Elements()
													  where Regex.Match(taxon, item.Value).Success
													  select item.Value;
					foreach (string item in queryResult)
					{
						xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, "<tn [^>]*>((?i)" + item + "(\\s+.*?)?(\\.?))</tn>", "$1");
					}
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Apply black list.");
			}
		}

		private void ApplyWhiteList()
		{
			try
			{
				XElement whiteList = XElement.Load(config.whiteListXmlFilePath);
				IEnumerable<string> whiteListItems = from item in whiteList.Elements()
													 select item.Value;
				foreach (string item in whiteListItems)
				{
					xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, "(?<!<tn [^>]*>)(?<!name [^>]*>)(?<!<[^>]+=\"[^>]*)(?i)\\b(" + item + ")\\b(?!\"\\s?>)(?!</tn)(?!</tp:)", higherTaxaReplacePattern);
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Applying white list.");
			}
		}

		public void FormatTreatments()
		{
			ParseXmlStringToXmlDocument();
			try
			{
				foreach (XmlNode node in xmlDocument.SelectNodes("//tp:nomenclature", namespaceManager))
				{
					if (node["title"] != null)
					{
						node["title"].InnerXml = Regex.Replace(node["title"].InnerXml, "</?italic>", "");
						node["title"].InnerXml = Regex.Replace(node["title"].InnerXml, "\\s+", " ");
					}
					node.InnerXml = Regex.Replace(node.InnerXml, @"(?<=<title [^>]*>)\s+|\s+(?=</title>)", "");

					string replace = node.InnerXml;

					/*
					 * Extract label preceding lower taxa and Authority and Status tags
					 */
					replace = Regex.Replace(replace,
						@"(\s*)<title[^>]*>([^<>]+?)\s*(<tp:taxon-name [^>]*>.*?</tp:taxon-name>)\s*([^<>]*)</title>",
						"$1<label>$2</label>$1$3$1<tp:taxon-authority>$4</tp:taxon-authority>");
					/*
					 * Extract Authority and Status tags if there is no label
					 */
					replace = Regex.Replace(replace,
						@"(\s*)<title[^>]*>(<tp:taxon-name [^>]*>.*?</tp:taxon-name>)\s*([^<>]*)</title>",
						"$1$2$1<tp:taxon-authority>$3</tp:taxon-authority>");

					replace = Regex.Replace(replace, @"\s*<tp:taxon-authority>\s*</tp:taxon-authority>", "");
					/*
					 * Format nomenclature
					 */
					replace = Regex.Replace(replace,
						@"(?<=</label>)(\s*)(<tp:taxon-name [^>]*>)(<tp:taxon-name-part[\s\S]*?)(</tp:taxon-name>)",
						"$1$2$1    $3$1$4");
					replace = Regex.Replace(replace,
						@"^(\s*)(<tp:taxon-name [^>]*>)(<tp:taxon-name-part[\s\S]*?)(</tp:taxon-name>)",
						"$1$2$1    $3$1$4");
					for (int i = 0; i < 8; i++)
					{
						replace = Regex.Replace(replace,
							@"(\n\s*)(\(?<tp:taxon-name-part [^>]*>.*?</tp:taxon-name-part>\)?) (\(?<tp:taxon-name-part [^>]*>.*?</tp:taxon-name-part>\)?)",
							"$1$2$1$3");
					}

					/*
					 * Split authority and status
					 */
					replace = Regex.Replace(replace,
						@"<tp:taxon-authority>((([a-z]+\.(\s*)(n|nov))|(n\.\s*[a-z]+)|(([a-z]+\.)?(\s*)spp))(\.)?|new record)</tp:taxon-authority>",
						"<tp:taxon-status>$1</tp:taxon-status>");
					replace = Regex.Replace(replace,
						@"(\s*)<tp:taxon-authority>([\w\-\,\;\.\(\)\&\s-]+)(\s*\W\s*)([Ii]ncertae\s+[Ss]edis|nom\.?\s+cons\.?|[a-z]+\.\s*(n|nov|r|rev)(\.)?|new record)</tp:taxon-authority>",
						"$1<tp:taxon-authority>$2</tp:taxon-authority>$1<tp:taxon-status>$4</tp:taxon-status>");
					replace = Regex.Replace(replace, @"(<tp:taxon-authority>.*)((?<!&[a-z]+)[\s,;:]+)(</tp:taxon-authority>)", "$1$3");
					replace = Regex.Replace(replace, @"(<tp:taxon-authority>.*?</tp:taxon-authority>\S*)\s+?(\n?)", "$1\n");
					replace = Regex.Replace(replace, @"(?<=<tp:taxon-authority>)\s+|\s+(?=</tp:taxon-authority>)", "");

					node.InnerXml = replace;

					if (node["object-id"] != null)
					{
						if (node["tp:taxon-name"] != null)
						{
							foreach (XmlNode objectId in node.SelectNodes("./object-id", namespaceManager))
							{
								node["tp:taxon-name"].AppendChild(objectId);
							}
							node["tp:taxon-name"].InnerXml = Regex.Replace(node["tp:taxon-name"].InnerXml, "(?=<object-id)", "    ");
						}
						node.InnerXml = Regex.Replace(node.InnerXml, @"\n\s*\n", "\n");
					}
				}

				xmlDocument.InnerXml = Regex.Replace(xmlDocument.InnerXml, @"(\s*)(    <object-id .*?</object-id>)(</tp:taxon-name>)", "$1$2$1$3");
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		private const string selectTreatmentGeneraXPathString = "//tp:taxon-treatment[string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order'])='ORDO' or string(tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family'])='FAMILIA']/tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='genus']";

		public void ParseTreatmentMetaWithAphia()
		{
			ParseXmlStringToXmlDocument();
			try
			{
				List<string> genusList = Base.GetStringListOfUniqueXmlNodes(xmlDocument, selectTreatmentGeneraXPathString, namespaceManager);

				bool delay = false;
				foreach (string genus in genusList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}

					Console.WriteLine("\n{0}\n", genus);

					XmlDocument response = Net.SearchAphia(genus);

					List<string> responseFamily = Base.GetStringListOfUniqueXmlNodes(response, "//return/item[string(genus)='" + genus + "']/family", namespaceManager);
					List<string> responseOrder = Base.GetStringListOfUniqueXmlNodes(response, "//return/item[string(genus)='" + genus + "']/order", namespaceManager);
					List<string> responseKingdom = Base.GetStringListOfUniqueXmlNodes(response, "//return/item[string(genus)='" + genus + "']/kingdom", namespaceManager);

					TreatmentMetaReplaceKingdoms(responseKingdom, genus);
					TreatmentMetaReplaceOrders(responseOrder, genus);
					TreatmentMetaReplaceFamilies(responseFamily, genus);
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		public void ParseTreatmentMetaWithGbif()
		{
			ParseXmlStringToXmlDocument();
			try
			{
				List<string> genusList = Base.GetStringListOfUniqueXmlNodes(xmlDocument, selectTreatmentGeneraXPathString, namespaceManager);

				bool delay = false;
				foreach (string genus in genusList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}
					Console.WriteLine("\n{0}\n", genus);

					Json.Gbif.GbifResult obj = Net.SearchGbif(genus);
					if (obj != null)
					{
						Console.WriteLine("{0} .... {1} .... {2}", genus, obj.scientificName, obj.canonicalName);

						if (obj.canonicalName != null || obj.scientificName != null)
						{
							if (!obj.canonicalName.Equals(genus) && !obj.scientificName.Contains(genus))
							{
								Alert.Message("No match.");
							}
							else
							{
								Alert.Message("Kingdom: " + obj.kingdom);
								Alert.Message("Order: " + obj.order);
								Alert.Message("Family: " + obj.family);
								Alert.Message();

								List<string> responseKingdom = new List<string>();
								List<string> responseOrder = new List<string>();
								List<string> responseFamily = new List<string>();

								responseKingdom.Add(obj.kingdom);
								responseOrder.Add(obj.order);
								responseFamily.Add(obj.family);

								if (obj.alternatives != null)
								{
									foreach (var alternative in obj.alternatives)
									{
										if (alternative.canonicalName.Equals(genus) || alternative.scientificName.Contains(genus))
										{
											responseKingdom.Add(alternative.kingdom);
											responseOrder.Add(alternative.order);
											responseFamily.Add(alternative.family);
										}
									}
								}

								TreatmentMetaReplaceKingdoms(responseKingdom.Cast<string>().Select(c => c).Distinct().ToList(), genus);
								TreatmentMetaReplaceOrders(responseOrder.Cast<string>().Select(c => c).Distinct().ToList(), genus);
								TreatmentMetaReplaceFamilies(responseFamily.Cast<string>().Select(c => c).Distinct().ToList(), genus);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		public void ParseTreatmentMetaWithCoL()
		{
			ParseXmlStringToXmlDocument();
			try
			{
				List<string> genusList = Base.GetStringListOfUniqueXmlNodes(xmlDocument, selectTreatmentGeneraXPathString, namespaceManager);

				bool delay = false;
				foreach (string genus in genusList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}
					Console.WriteLine("\n{0}\n", genus);

					XmlDocument response = Net.SearchCatalogueOfLife(genus);

					if (response != null)
					{
						List<string> responseFamily = Base.GetStringListOfUniqueXmlNodes(response, "/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Family']/name", namespaceManager);
						List<string> responseOrder = Base.GetStringListOfUniqueXmlNodes(response, "/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Order']/name", namespaceManager);
						List<string> responseKingdom = Base.GetStringListOfUniqueXmlNodes(response, "/results/result[string(name)='" + genus + "']/classification/taxon[string(rank)='Kingdom']/name", namespaceManager);

						TreatmentMetaReplaceKingdoms(responseKingdom, genus);
						TreatmentMetaReplaceOrders(responseOrder, genus);
						TreatmentMetaReplaceFamilies(responseFamily, genus);

						// Some debug information
						foreach (string x in responseKingdom)
						{
							Alert.Message("Kingdom: " + x);
						}

						Alert.Message();
						foreach (string x in responseOrder)
						{
							Alert.Message("Order: " + x);
						}

						Alert.Message();
						foreach (string x in responseFamily)
						{
							Alert.Message("Family: " + x);
						}

						Alert.Message();
					}
				}
			}
			catch (Exception e)
			{
				Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0);
			}

			xml = xmlDocument.OuterXml;
		}

		private void TreatmentMetaReplaceKingdoms(List<string> responseKingdom, string genus)
		{
			if (responseKingdom.Count == 1)
			{
				string kingdom = responseKingdom[0];
				foreach (XmlNode node in xmlDocument.SelectNodes("//tp:taxon-treatment[string(tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='genus'])='" + genus + "']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='kingdom']", namespaceManager))
				{
					node.InnerXml = kingdom;
				}
			}
			else
			{
				Alert.Message("WARNING: Multiple or zero kingdoms:");
				foreach (string kingdom in responseKingdom)
				{
					Alert.Message(kingdom);
				}
				Alert.Message();
			}
		}

		private void TreatmentMetaReplaceOrders(List<string> responseOrder, string genus)
		{
			if (responseOrder.Count == 1)
			{
				string order = responseOrder[0];
				foreach (XmlNode node in xmlDocument.SelectNodes("//tp:taxon-treatment[string(tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='genus'])='" + genus + "']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='order']", namespaceManager))
				{
					node.InnerText = order;
				}
			}
			else
			{
				Alert.Message("WARNING: Multiple or zero orders:");
				foreach (string order in responseOrder)
				{
					Alert.Message(order);
				}
				Alert.Message();
			}
		}

		private void TreatmentMetaReplaceFamilies(List<string> responseFamily, string genus)
		{
			if (responseFamily.Count == 1)
			{
				string family = responseFamily[0];
				foreach (XmlNode node in xmlDocument.SelectNodes("//tp:taxon-treatment[string(tp:nomenclature/tp:taxon-name/tp:taxon-name-part[@taxon-name-part-type='genus'])='" + genus + "']/tp:treatment-meta/kwd-group/kwd/named-content[@content-type='family']", namespaceManager))
				{
					node.InnerText = family;
				}
			}
			else
			{
				Alert.Message("WARNING: Multiple or zero families:");
				foreach (string family in responseFamily)
				{
					Alert.Message(family);
				}
				Alert.Message();
			}
		}

		// Flora-like tagging methods
		public void PerformFloraReplace(string xmlTemplate)
		{
			NormalizeXmlToSystemXml();

			ParseXmlStringToXmlDocument();

			XmlDocument template = new XmlDocument();
			template.LoadXml(xmlTemplate);

			XmlNode root = template.DocumentElement;
			Alert.Message(root.ChildNodes.Count);

			xml = xmlDocument.OuterXml;
			for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
			{
				XmlNode taxon = root.ChildNodes.Item(i);
				XmlNode find = taxon.FirstChild;
				XmlNode replace = taxon.LastChild;

				Alert.Message(find.InnerXml);

				string pattern = find.InnerXml;
				pattern = Regex.Replace(pattern, @"\.", "\\.");
				pattern = Regex.Replace(pattern, @"(?<=\w)\s+(?=\w)", @"\b\s*\b");
				pattern = Regex.Replace(pattern, @"(?<=\W)\s+(?=\w)", @"?\s*\b");
				pattern = Regex.Replace(pattern, @"(?<=\W)\s+", @"?\s*");
				pattern = Regex.Replace(pattern, @"\bvar\b", "(var|v)");

				pattern = "(?i)" + pattern;

				xml = Regex.Replace(xml,
					"(?<![a-z-])(?<!<[^>]+=\"[^>]*)(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
					"<tn>$1</tn>");
			}

			//string infraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
			//string lowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

			//xml = Regex.Replace(xml, infraspecificPattern + "\\s*<tn>", "<tn>$1 ");
			//xml = Regex.Replace(xml, "(?<!<tn>)(" + infraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

			//xml = Regex.Replace(xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
			//xml = Regex.Replace(xml, "(<tn>)" + infraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

			//xml = Regex.Replace(xml, "</tn>\\s*<tn>" + infraspecificPattern, " $1");

			//// TODO: Here we must remove tn/tn
			//{
			//    ParseXmlStringToXmlDocument();
			//    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
			//    foreach (XmlNode node in nodeList)
			//    {
			//        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
			//    }
			//    xml = xmlDocument.OuterXml;
			//}

			//// Guess new taxa:
			//for (int i = 0; i < 10; i++)
			//{
			//    xml = Regex.Replace(xml,
			//        "(</tn>,?(\\s+and)?\\s+)(" + infraspecificPattern + "?" + lowerPattern + ")",
			//        "$1<tn>$3</tn>");
			//}

			// Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
			//xml = Regex.Replace(xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

			//xml = Regex.Replace(xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + infraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

			//xml = Regex.Replace(xml,
			//    "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + infraspecificPattern + ")?" + lowerPattern + ")",
			//    "<tn>$1</tn>");

			//// TODO: Here we must remove tn/tn
			//{
			//    ParseXmlStringToXmlDocument();
			//    XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
			//    foreach (XmlNode node in nodeList)
			//    {
			//        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
			//    }
			//    xml = xmlDocument.OuterXml;
			//}

			//// Remove taxa in toTaxon
			//{
			//    ParseXmlStringToXmlDocument();
			//    XmlNodeList nodeList = xmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
			//    foreach (XmlNode node in nodeList)
			//    {
			//        node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", "");
			//    }
			//    xml = xmlDocument.OuterXml;
			//}

			if (config.NlmStyle)
			{
				xml = Base.NormalizeSystemToNlmXml(config, xml);
			}
		}
	}
}
