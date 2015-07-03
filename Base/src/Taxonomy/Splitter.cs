using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Base
{
	namespace Taxonomy
	{
		public class Splitter : Base
		{
			public Splitter()
				: base()
			{
			}

			public Splitter(string xml)
				: base(xml)
			{
			}

			public Splitter(Config config)
				: base(config)
			{
			}

			/*
			 * ================================================================================================
			 * 
			 * Lower taxa related methods
			 * 
			 * ================================================================================================
			 */

			public void SplitLowerTaxa()
			{
				xml = Format.Format.NormalizeNlmToSystemXml(config, xml);

				ParseXmlStringToXmlDocument();

				try
				{
					foreach (XmlNode node in xmlDocument.SelectNodes("//tn[@type='lower'][not(*)]", namespaceManager))
					{
						node.InnerXml = SplitLower(node.InnerXml);
					}
				}
				catch (Exception e)
				{
					Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Split lower taxa without basionym.");
				}

				try
				{
					foreach (XmlNode node in xmlDocument.SelectNodes("//tn[@type='lower'][count(*) != count(tn-part)]", namespaceManager))
					{
						string replace = Regex.Replace(node.InnerXml, "</?i>", "");
						string splitBasionym = Regex.Replace(replace, "^.*?<basionym>(.*?)</basionym>.*$", "$1");
						splitBasionym = SplitLower(splitBasionym);
						string splitSpecific = Regex.Replace(replace, "^.*?<specific>(.*?)</specific>.*$", "$1");
						splitSpecific = SplitLower(splitSpecific);

						replace = Regex.Replace(replace, "<genus>(.+?)</genus>", "<tn-part type=\"genus\">$1</tn-part>");
						replace = Regex.Replace(replace, "<genus-authority>(.*?)</genus-authority>", "<tn-part type=\"authority\">$1</tn-part>");
						replace = Regex.Replace(replace, "<basionym>(.*?)</basionym>", splitBasionym);
						replace = Regex.Replace(replace, "<specific>(.*?)</specific>", splitSpecific);
						replace = Regex.Replace(replace, @"<basionym-authority>(\s*)(\(.*?\))(\s*)(.*?)</basionym-authority>", "$1<tn-part type=\"basionym-authority\">$2</tn-part>$3<tn-part type=\"authority\">$4</tn-part>");
						replace = Regex.Replace(replace, "<basionym-authority>(.*?)</basionym-authority>", "<tn-part type=\"authority\">$1</tn-part>");

						for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>\s*<species>[^<>]*</species>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
						{
							string replace1 = m.Value;
							string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", "");
							string rank = ParseRank(infraSpecificRank);
							replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
							replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
							replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part>");
							replace1 = Regex.Replace(replace1, @"<species>([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-z\s\.-]+)</species>", "<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");
							rank = replace1.Contains("type=\"subspecies\"") ? "subspecies" : "species";
							replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"authority\">$1</tn-part>");
							replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
						}
						for (Match m = Regex.Match(replace, @"<infraspecific-rank>[^<>]*</infraspecific-rank>\s*<infraspecific>[^<>]*</infraspecific>(\s*<authority>[^<>]*</authority>)?"); m.Success; m = m.NextMatch())
						{
							string replace1 = m.Value;
							string infraSpecificRank = Regex.Replace(Regex.Replace(replace, "^.*?<infraspecific-rank>([^<>]*)</infraspecific-rank>.*$", "$1"), "\\.", "");
							string rank = ParseRank(infraSpecificRank);
							replace1 = Regex.Replace(replace1, "<infraspecific-rank>([^<>]*)</infraspecific-rank>", "<tn-part type=\"infraspecific-rank\">$1</tn-part>");
							replace1 = Regex.Replace(replace1, "<infraspecific>([^<>]*)</infraspecific>", "<tn-part type=\"" + rank + "\">$1</tn-part>");
							replace1 = Regex.Replace(replace1, "<authority>([^<>]*)</authority>", "<tn-part type=\"" + rank + "-authority\">$1</tn-part>");
							replace = Regex.Replace(replace, Regex.Escape(m.Value), replace1);
						}
						replace = Regex.Replace(replace, @"<sensu>(.*?)</sensu>", "<tn-part type=\"sensu\">$1</tn-part>");
						replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">×</tn-part>", "<tn-part type=\"hybrid-sign\">×</tn-part>");
						replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">\?</tn-part>", "<tn-part type=\"uncertainty-rank\">?</tn-part>");
						replace = Regex.Replace(replace, @"<tn-part type=""infraspecific-rank"">((?i)(aff|prope|cf|nr|sp\. near)\.?)</tn-part>", "<tn-part type=\"uncertainty-rank\">$1</tn-part>");
						//replace = Regex.Replace(replace, "</tn-part><tn-part", "</tn-part> <tn-part");
						node.InnerXml = replace;
					}

					// Add @full-name
					foreach (XmlNode node in xmlDocument.SelectNodes("//tn[@type='lower']/tn-part[@type='genus' or @type='subgenus' or @type='species' or @type='subspecies'][not(@full-name)][contains(string(.), '.')]", namespaceManager))
					{
						XmlAttribute fullName = xmlDocument.CreateAttribute("full-name");
						node.Attributes.Append(fullName);
					}
				}
				catch (Exception e)
				{
					Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Split lower taxa with basionym.");
				}

				// Remove wrapping i around tn[tn-part[@type='subgenus']]
				xml = Regex.Replace(xmlDocument.OuterXml, @"<i>(<tn(\s*>|\s[^<>]*>)<tn-part type=""genus""[^<>]*>[^<>]*</tn-part>\s*\(<tn-part type=""(subgenus|superspecies)""[^<>]*>.*?</tn>)</i>", "$1");

				if (config.NlmStyle)
				{
					xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
				}
			}

			private string ParseRank(string infraSpecificRank)
			{
				string rank = string.Empty;
				switch (infraSpecificRank)
				{
					case "subgen":
					case "Subgen":
					case "subgenus":
					case "Subgenus":
					case "subg":
					case "Subg":
						rank = "subgenus";
						break;
					case "sect":
					case "Sect":
					case "section":
					case "Section":
						rank = "section";
						break;
					case "Ser":
					case "ser":
						rank = "series";
						break;
					case "subsect":
					case "Subsect":
					case "subsection":
					case "Subsection":
						rank = "subsection";
						break;
					case "subvar":
					case "Subvar":
						rank = "subvariety";
						break;
					case "var":
					case "Var":
						rank = "variety";
						break;
					case "subsp":
					case "Subsp":
					case "ssp":
					case "Ssp":
					case "subspec":
					case "Subspec":
					case "Subspecies":
					case "subspecies":
						rank = "subspecies";
						break;
					case "st":
					case "St":
						rank = "stage";
						break;
					case "r":
						rank = "rank";
						break;
					case "f":
					case "fo":
					case "form":
					case "Form":
					case "forma":
					case "Forma":
						rank = "form";
						break;
					case "sf":
					case "Sf":
						rank = "subform";
						break;
					case "aff":
					case "Aff":
					case "prope":
					case "Prope":
					case "cf":
					case "Cf":
					case "sp":
					case "Sp":
					case "nr":
					case "Nr":
					case "sp. near":
					case "×":
					case "?":
						rank = "species";
						break;
					case "a":
					case "A":
					case "ab":
					case "Ab":
						rank = "aberration";
						break;
					default:
						rank = "species";
						break;
				}
				return rank;
			}

			private string SplitLower(string str)
			{
				string replace = str;

				// Parse different parts of the taxonomic name
				replace = Regex.Replace(replace,
					@"\b([Ss]ubvar)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subvariety\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Vv]ar|v)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"variety\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Ff]orma?|f)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"form\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b(sf|[Ss]ubforma?)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subform\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Aa]b)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"aberration\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Ss]ubsp|[Ss]sp)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subspecies\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Aa]ff|[Cc]f|[Nn]r|[Ss]p|[Ss]p\.?\s*ne?a?r|[Ss]p\s+ne?a?r)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"uncertainty-rank\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Ss]ubgen|[Ss]ubgenus|[Ss]g)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subgenus\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Ss]ect|[Ss]ection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"section\">$3</tn-part>");

				replace = Regex.Replace(replace,
					@"\b([Ss]ubsect|[Ss]ubsection)\b(\.\s*|\s+)([A-Za-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"infraspecific-rank\">$1</tn-part>$2<tn-part type=\"subsection\">$3</tn-part>");

				// Parse taxa from beginnig

				// Genus species subspecies
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>$4<tn-part type=\"subspecies\">$5</tn-part>");
				// Genus species
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2<tn-part type=\"species\">$3</tn-part>");
				replace = Regex.Replace(replace,
					@"\A‘([A-Z][a-z\.]+)’([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"‘<tn-part type=\"genus\">$1</tn-part>’$2<tn-part type=\"species\">$3</tn-part>");

				// Genus (Subgenus) species subspecies
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");
				// Genus (superspecies) species subspecies
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([a-z][a-zçäöüëïâěôûêî\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>$7<tn-part type=\"subspecies\">$8</tn-part>");
				// Genus (Subgenus) species
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");
				// Genus (superspecies) species
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([a-z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)([\s\?×]+)([A-Z]?[a-zçäöüëïâěôûêîæœ\.-]+)",
					"<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"superspecies\">$3</tn-part>$4)$5<tn-part type=\"species\">$6</tn-part>");
				// Genus (Subgenus)
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)([\s\?×]+)\(\s*([A-Z][a-zçäöüëïâěôûêîæœ\.-]+)([\s\?×]*?)\s*\)",
					"<tn-part type=\"genus\">$1</tn-part>$2(<tn-part type=\"subgenus\">$3</tn-part>$4)");
				// Genus
				replace = Regex.Replace(replace,
					@"\A([A-Z][a-z\.]+\-[A-Z][a-z\.]+|[A-Z][a-z\.]+)",
					"<tn-part type=\"genus\">$1</tn-part>");
				// species
				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)",
					@"<tn-part type=""species"">$1</tn-part>");

				// Try to parse whole string

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]sp|[Ss]ubsp)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subspecies\">$4</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Vv]ar|[Vv])(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"variety\">$4</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Aa]b)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"aberration\">$4</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ff]|[Ff]orma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"form\">$4</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([Ss]f|[Ss]ubforma?)(\.\s*|\s+)([A-Za-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"infraspecific-rank\">$2</tn-part>$3<tn-part type=\"subform\">$4</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\s+([a-zçäöüëïâěôûêî-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([a-zçäöüëïâěôûêîæœ\.-]+)\Z",
					"<tn-part type=\"species\">$1</tn-part>");

				replace = Regex.Replace(replace,
					@"\A([A-Z\.-]+)\Z",
					"<tn-part type=\"genus\">$1</tn-part>");


				// Parse question mark
				replace = Regex.Replace(replace, @"(?<=</tn-part>)\s*\?", "<tn-part type=\"uncertainty-rank\">?</tn-part>");

				// Parse hybrid sign
				replace = Regex.Replace(replace, @"(?<=</tn-part>\s*)×(?=\s*<tn-part)", "<tn-part type=\"hybrid-sign\">×</tn-part>");

				// Paste @full-name of shortened taxonomic names
				replace = Regex.Replace(replace, @"(<tn-part [^>]*)(>\w+\.)", "$1 full-name=\"\"$2");

				// Here we must return the dot after tn-part[@type='infraspecific-rank'] back into the tag
				replace = Regex.Replace(replace, @"</tn-part>\.", ".</tn-part>");

				// Clear multiple white spaces between ‘tn-part’-s
				replace = Regex.Replace(replace, @"(?<=</tn-part>)\s{2,}(?=<tn-part)", " ");

				// Add some empty tag for missing taxa of higher ranks than current highest rank
				replace = Regex.Replace(replace,
					@"\A(\s*<tn-part [^<>]*type=""(species)""[^<>]*>)",
					"<tn-part type=\"genus\" full-name=\"\"/>$1");

				replace = Regex.Replace(replace,
					@"\A(\s*<tn-part [^<>]*type=""infraspecific-rank""[^<>]*>)",
					"<tn-part type=\"genus\" full-name=\"\"/><tn-part type=\"species\" full-name=\"\"/>$1");

				return replace;
			}

			/*
			 * ================================================================================================
			 * 
			 * Higher taxa related methods
			 * 
			 * ================================================================================================
			 */

			/// <summary>
			/// This is the main method to split higher taxa. It applies split-using-local-database-file by default.
			/// </summary>
			public void SplitHigherTaxa(
				bool splitWithDatabaseXmlFile = true,
				bool splitWithAphiaApi = false,
				bool splitWithCoLApi = false,
				bool splitWithGbifApi = false,
				bool splitBySuffix = false,
				bool splitAboveGenus = false)
			{
				xml = Format.Format.NormalizeNlmToSystemXml(config, xml);

				ParseXmlStringToXmlDocument();

				if (splitWithDatabaseXmlFile)
				{
					xmlDocument = Splitter.SplitHigherTaxaWithDatabaseXmlFile(config.rankListXmlFilePath, xmlDocument, config.NlmStyle);
				}
				if (splitWithAphiaApi)
				{
					xmlDocument = Splitter.SplitHigherTaxaWithAphiaApi(xmlDocument);
				}
				if (splitWithCoLApi)
				{
					xmlDocument = Splitter.SplitHigherTaxaWithCoLApi(xmlDocument);
				}
				if (splitWithGbifApi)
				{
					xmlDocument = Splitter.SplitHigherTaxaWithGbifApi(xmlDocument);
				}
				if (splitBySuffix)
				{
					xmlDocument = Splitter.SplitHigherTaxaBySuffix(xmlDocument, config.NlmStyle);
				}
				if (splitAboveGenus)
				{
					xmlDocument = Splitter.SplitHigherTaxaWithAboveGenus(xmlDocument);
				}

				// Some debug information
				{
					List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
					if (stringList.Count > 0)
					{
						Alert.Message("\nNon-split taxa:");
						foreach (string s in stringList)
						{
							Alert.Message("\t" + s);
						}
						Alert.Message();
					}
				}

				xml = xmlDocument.OuterXml;

				if (config.NlmStyle)
				{
					xml = Format.Format.NormalizeSystemToNlmXml(config, xml);
				}
			}

			public static string[] HigherTaxaRanks = {
														"phylum",
														"subphylum",
														"class",
														"subclass",
														"superorder",
														"order",
														"suborder",
														"infraorder",
														"superfamily",
														"epifamily",
														"family",
														"subfamily",
														"tribe",
														"subtribe"
													};

			public static string[] HigherTaxaSuffixes = {
														"phyta|mycota",
														"phytina|mycotina",
														"ia|opsida|phyceae|mycetes",
														"idae|phycidae|mycetidae",
														"anae",
														"ales",
														"ineae",
														"aria",
														"acea|oidea",
														"oidae",
														"aceae|idae",
														"oideae|inae",
														"eae|ini",
														"inae|ina"
														};

			public static XmlDocument SplitHigherTaxaBySuffix(XmlDocument xmlDocument, bool nlmStyle)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				string patternPrefix = "^([A-Z][a-z]*", patternSuffix = ")$";
				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				foreach (string text in stringList)
				{
					Alert.Message(text);
					string replace = text;
					for (int i = 0; i < HigherTaxaRanks.Length; i++)
					{
						replace = Regex.Replace(replace,
							patternPrefix + HigherTaxaSuffixes[i] + patternSuffix,
							tnpart.TaxonNamePartReplace(HigherTaxaRanks[i]));
					}
					xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
					xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(text) + ")(?=</tp:taxon-name>)", replace);
				}
				return xmlResult;
			}

			public static XmlDocument SplitHigherTaxaWithDatabaseXmlFile(string databaseXmlFileName, XmlDocument xmlDocument, bool nlmStyle)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				XmlDocument rankList = new XmlDocument();
				rankList.Load(databaseXmlFileName);

				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				foreach (string text in stringList)
				{
					XmlNodeList rankNodeList = rankList.SelectNodes("//taxon/part[translate(value,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='" + text.ToLower() + "']");
					if (rankNodeList.Count == 0)
					{
						Alert.Message(); Alert.Message(text + " --> No match.");
					}
					else if (rankNodeList.Count > 1)
					{
						Alert.Message(text);
						Alert.Message("WARNING: More than one records in local database.");
						Alert.Message("         Substitution will not be performed.");
						foreach (XmlNode node in rankNodeList)
						{
							Alert.Message(); Alert.Message(node.OuterXml);
						}
					}
					else
					{
						Alert.Message(); Alert.Message(text + " --> " + rankNodeList[0]["rank"]["value"].InnerText);
						string replace = tnpart.TaxonNamePartReplace(rankNodeList[0]["rank"]["value"].InnerText);
						xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
						xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(text) + ")(?=</tp:taxon-name>)", replace);
					}
				}
				return xmlResult;
			}

			public static XmlDocument SplitHigherTaxaWithGbifApi(XmlDocument xmlDocument)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				bool delay = false;
				foreach (string text in stringList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}
					Json.Gbif.GbifResult obj = Net.SearchGbif(text);
					if (obj != null)
					{
						Console.WriteLine("\n{0} .... {1} .... {2}", text, obj.scientificName, obj.canonicalName);

						if (obj.canonicalName != null || obj.scientificName != null)
						{
							if (!obj.canonicalName.Equals(text) && !obj.scientificName.Contains(text))
							{
								Alert.Message("No match.");
							}
							else
							{
								if (obj.rank != null)
								{
									Alert.Message("--> " + obj.rank.ToLower());
									string replace = tnpart.TaxonNamePartReplace(obj.rank.ToLower());
									xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
									xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(text) + ")(?=</tp:taxon-name>)", replace);
								}
							}
						}

					}
				}
				return xmlResult;
			}

			public static XmlDocument SplitHigherTaxaWithAphiaApi(XmlDocument xmlDocument)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				bool delay = false;
				foreach (string scientificName in stringList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}

					XmlDocument response = Net.SearchAphia(scientificName);
					XmlNodeList responseItems = response.SelectNodes("//return/item[normalize-space(translate(scientificname,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
					if (responseItems.Count < 1)
					{
						Alert.Message(scientificName + " --> No match or error.");
					}
					else
					{
						List<string> ranks = responseItems.Cast<XmlNode>().Select(c => c["rank"].InnerText.ToLower()).Distinct().ToList();
						if (ranks.Count > 1)
						{
							Alert.Message("WARNING:\n" + scientificName + " --> Multiple matches:");
							foreach (XmlNode item in responseItems)
							{
								Alert.Message(item["scientificname"].InnerText + " --> " + item["rank"].InnerText + ", " + item["authority"].InnerText);
							}
						}
						else
						{
							string rank = ranks[0];
							Alert.Message(scientificName + " = " + responseItems[0]["scientificname"].InnerText + " --> " + rank);
							string replace = tnpart.TaxonNamePartReplace(rank);
							xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replace);
							xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tp:taxon-name>)", replace);
						}
					}
				}
				return xmlResult;
			}

			public static XmlDocument SplitHigherTaxaWithCoLApi(XmlDocument xmlDocument)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				bool delay = false;
				foreach (string scientificName in stringList)
				{
					if (delay)
					{
						System.Threading.Thread.Sleep(15000);
					}
					else
					{
						delay = true;
					}

					XmlDocument response = Net.SearchCatalogueOfLife(scientificName);
					Alert.Message();
					Alert.Message(response.OuterXml);
					Alert.Message();

					XmlNodeList responseItems = response.SelectNodes("/results/result[normalize-space(translate(name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='" + scientificName.ToLower() + "']");
					if (responseItems.Count < 1)
					{
						Alert.Message(scientificName + " --> No match or error.");
					}
					else
					{
						List<string> ranks = responseItems.Cast<XmlNode>().Select(c => c["rank"].InnerText.ToLower()).Distinct().ToList();
						if (ranks.Count > 1)
						{
							Alert.Message("WARNING:\n" + scientificName + " --> Multiple matches:");
							foreach (XmlNode item in responseItems)
							{
								Alert.Message(item["name"].InnerText + " --> " + item["rank"].InnerText);
							}
						}
						else
						{
							string rank = ranks[0].ToLower();
							Alert.Message(scientificName + " = " + responseItems[0]["name"].InnerText + " --> " + rank);
							string replace = tnpart.TaxonNamePartReplace(rank);
							xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tn>)", replace);
							xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(scientificName) + ")(?=</tp:taxon-name>)", replace);
						}
					}
				}
				return xmlResult;
			}

			/// <summary>
			/// This method splits all non-parsed higher taxa by making then of type 'above-genus'
			/// </summary>
			/// <param name="xmlDocument">Xml document to be processed</param>
			/// <returns>Processed xml document</returns>
			public static XmlDocument SplitHigherTaxaWithAboveGenus(XmlDocument xmlDocument)
			{
				XmlDocument xmlResult = xmlDocument;
				TaxonNamePart tnpart = new TaxonNamePart();
				List<string> stringList = ExtractUniqueHigherTaxa(xmlDocument);
				foreach (string text in stringList)
				{
					Alert.Message(); Alert.Message(text + " --> above-genus");
					string replace = tnpart.TaxonNamePartReplace("above-genus");
					xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tn [^>]*>)(" + Regex.Escape(text) + ")(?=</tn>)", replace);
					xmlResult.InnerXml = Regex.Replace(xmlResult.InnerXml, "(?<=<tp:taxon-name [^>]*>)(" + Regex.Escape(text) + ")(?=</tp:taxon-name>)", replace);
				}
				return xmlResult;
			}

			public static List<string> ExtractUniqueHigherTaxa(XmlDocument xmlDocument)
			{
				XmlNamespaceManager xmlNamespaceManager = Base.TaxPubNamespceManager(xmlDocument);
				XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[@type='higher'][not(tn-part)]", xmlNamespaceManager);
				return nodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
			}

			public void UnSplitAllTaxa()
			{
				ParseXmlStringToXmlDocument();
				foreach (XmlNode node in xmlDocument.SelectNodes("//tn|//tp:taxon-name[name(..)!='tp:nomenclature']", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "( full-name=\"(.*?)\"[^>]*>)[^<>]*(?=</)", "$1$2");
					node.InnerXml = Regex.Replace(node.InnerXml, "</?tn-part[^>]*?>|</?tp:taxon-name-part[^>]*?>", "");
				}
				xml = xmlDocument.OuterXml;
			}

			public static string UnSplitTaxa(string content)
			{
				string result = Regex.Replace(content, "( full-name=\"(.*?)\"[^>]*>)([^<>]*)(?=</)", "$1$2");
				return Regex.Replace(result, "</?tn-part[^>]*?>|</?tp:taxon-name-part[^>]*?>", "");
			}
		}

		class TaxonNamePart
		{
			private string prefix, suffix;
			bool taxPub;

			public TaxonNamePart(bool taxPub = false)
			{
				this.taxPub = taxPub;
				if (taxPub)
				{
					prefix = "<tp:taxon-name-part taxon-name-part-type=\"";
					suffix = "\">$1</tp:taxon-name-part>";
				}
				else
				{
					prefix = "<tn-part type=\"";
					suffix = "\">$1</tn-part>";
				}
			}

			public string Prefix
			{
				get
				{
					return prefix;
				}
			}

			public string Suffix
			{
				get
				{
					return suffix;
				}
			}

			public string TaxonNamePartReplace(string rank)
			{
				return prefix + rank + suffix;
			}
		}
	}
}
