using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base.Format
{
	namespace Nlm
	{
		public class Format : Base
		{
			public Format() : base() { }
			public Format(string xml) : base(xml) { }


			public void InitialFormat()
			{
				xml = Regex.Replace(xml, "\r", "");

				// Remove white spaces in @id and @rid values
				xml = Regex.Replace(xml, @"(?<=<[^>]+\br?id="")\s*([^<>""]*?)\s*(?="")", "$1");

				// Format openeing and closing tags
				FormatCloseTags();
				FormatOpenTags();
				BoldItalic();

				// Format wrong figures' labels
				xml = Regex.Replace(xml, @"<caption>\s+<p>", "<caption><p>");
				xml = Regex.Replace(xml, @"</p>\s+</caption>", "</p></caption>");
				xml = Regex.Replace(xml, @"(\s*)(<caption><p>)\s*<b>\s*((Figure|Map|Plate|Table|Suppl|Box)[^<>]*?)\s*</b>", "$1<label>$3</label>$1$2");

				FormatReferances();
				FormatPageBreaks();

				// male and female
				xml = Regex.Replace(xml, "<i>([♂♀\\s]+)</i>", "$1");

				// Post-formatting
				for (int i = 0; i < 3; i++)
				{
					BoldItalic();
					FormatPunctuation();
					//FormatPageBreaks();
					RemoveEmptyTags();
					FormatCloseTags();
					FormatOpenTags();
				}

				ParseXmlStringToXmlDocument();
				foreach (XmlNode node in xmlDocument.SelectNodes("//title|//label|//article-title|//th|//td|//p|//license-p|//li|//attrib|//kwd|//ref|//mixed-citation|//object-id|//xref-group|//tp:nomenclature-citation|//self-uri|//name|//given-names|//surname|//person-group|//graphic[string()!='']", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, @"\A\s+|\s+\Z", "");
					node.InnerXml = Regex.Replace(node.InnerXml, @"\s+", " ");
				}
				foreach (XmlNode node in xmlDocument.SelectNodes("//td|//th", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml, "&lt;br/&gt;", "<break />");
				}
				foreach (XmlNode node in xmlDocument.SelectNodes("//front//contrib", namespaceManager))
				{
					node.InnerXml = Regex.Replace(node.InnerXml,
						@"(\s*)(<name [^>]*>)\s*(<surname>.*?</surname>)\s*(<given-names>.*?</given-names>)\s*(</name>)",
						"$1$2$1    $3$1    $4$1$5");
				}
				xml = xmlDocument.OuterXml;

				// Subspecies, subgenera, etc.
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z]*\.\s+[a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
				xml = Regex.Replace(xml, @"(<i>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</i>)(\))", "$1$3$2");
				xml = Regex.Replace(xml, @"</i>\s+<i>", " ");
				xml = Regex.Replace(xml, @"</i><i>", "");

				// Clear empty symbols out of <article> tag
				xml = Regex.Replace(xml, @"(?<=</article>)\s+", "");

				// Supplementary materials external link
				xml = Regex.Replace(xml, "(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2");

				// Misc
				xml = Regex.Replace(xml, @"\s*(<\w[^>]*>\s*)?&lt;br\s*/&gt;(\s*</\w[^>]*>)?\s*(?=</p>\s*</caption>)", "");
				xml = Regex.Replace(xml, @"\.</i>(?=</p>)", "</i>.");

				// sensu lato & stricto
				xml = Regex.Replace(xml, @"(<i>)((s\.|sens?u?)\s+[sl][a-z]*)(</i>)\.", "$1$2.$4");
				//
				xml = Regex.Replace(xml, @"<\s+/", "</");
				//
				RemoveEmptyTags();
				FormatPageBreaks();

				xml = Regex.Replace(xml, "(\\s+)(<tp:nomenclature-citation-list>)(<tp:nomenclature-citation>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<ref-list>)(<title>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<publisher>)(<publisher-name>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<pub-date [^>]*>)(<)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<permissions>)(<copyright-statement>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<permissions>)(<license)", "$1$2$1    $3");
				xml = Regex.Replace(xml, @"(?<=(\s*)<license[^>]*>)(<license-p>)", "$1    $2");
				xml = Regex.Replace(xml, "(\\s+)(<kwd-group>)(<label>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, "(\\s+)(<title-group>)(<article-title>)", "$1$2$1    $3");
				xml = Regex.Replace(xml, @"(\s*)(</table>)\s*&lt;br\s*/&gt;\s*", "$1$2$1");

				// Remove empty lines
				xml = Regex.Replace(xml, "\n\\s*(?=\n)", "");
			}

			private void BoldItalicSpaces()
			{
				xml = Regex.Replace(xml, @"(<b>|<i>)(\s+)", " $1");
				xml = Regex.Replace(xml, @"\s+(</b>|</i>)", "$1 ");
				xml = Regex.Replace(xml, @"(</b>\s+<b>|</i>\s+<i>|<b>\s+</b>|<i>\s+</i>)", " ");
				xml = Regex.Replace(xml, @"(</b><b>|<b></b>|</i><i>|<i></i>)", "");
				xml = Regex.Replace(xml, @"(</italic>|</bold>)(\w)", "$1 $2");
				//xml = Regex.Replace(xml, @"(“|‘)\s+(<i>|<b>)", " $1$2");
				//xml = Regex.Replace(xml, @"(</i>|</b>)\s+(’)", "$1$2 ");

				xml = Regex.Replace(xml, @"([\(\[])\s+(<i>|<b>|<u>|<sub>|<sup>)\s*", " $1$2");
				//xml = Regex.Replace(xml, @"\s+(</i>|</b>|</u>|</sub>|</sup>)([^,;\)\]\.])", "$1 $2");

				//xml = Regex.Replace(xml, @"(</b>|</i>)\s*(<b>|<i>)", "$1 $2");

				xml = Regex.Replace(xml, @"([,\.;])(<i>|<b>)", "$1 $2");
				xml = Regex.Replace(xml, @"(</i>|</b>)\s+([,\.;])", "$1$2 ");
			}

			private void BoldItalic()
			{
				BoldItalicSpaces();

				xml = Regex.Replace(xml, @"\&lt;\s*br\s*/\s*\&gt;(</i>|</b>)", "$1&lt;br/&gt;");
				xml = Regex.Replace(xml, @"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</i>|</b>)", "$2$1");
				xml = Regex.Replace(xml, @"(<i>|<b>)([^\w<>\.\(\)\&]+)", "$2$1");
				xml = Regex.Replace(xml, @"(<i>)([A-Za-z][a-z]{0,2})(</i>)(\.)", "$1$2$4$3");
				//xml = Regex.Replace(xml, @"(</bold>)([\.:])", "$2$1");

				xml = Regex.Replace(xml, @"\s*\(\s*(</i>|</b>)", "$1 (");
				xml = Regex.Replace(xml, @"(<i>|<b>)\s*\)\s*", ") $1");

				//xml = Regex.Replace(xml, "(’)(</italic>)", "$2$1");
				//xml = Regex.Replace(xml, "(<italic>)(‘)", "$2$1");

				xml = Regex.Replace(xml, @"(<b>|<i>)([,\s\.:;\-––])(</b>|</i>)", "$2");

				// Genus + (Subgenus)
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Za-z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");
				// Genus + (Subgenus) + species
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");
				// sensu lato & sensu stricto
				xml = Regex.Replace(xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*.*?)</i>", "<i>$1</i> <i>$2</i>");
				xml = Regex.Replace(xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
				xml = Regex.Replace(xml, @"<i>(s(ensu|\.))\s*(l|s|str)</i>\.", "<i>$1 $3.</i>");

				// Paste some intervals
				//xml = Regex.Replace(xml, "</i><b>", "</i> <b>");

				xml = Regex.Replace(xml, @"(</i>)(\()", "$1 $2");
				xml = Regex.Replace(xml, @"(<i>)([\.,;:\s]+)", "$2$1");
			}

			private void FormatCloseTags()
			{
				xml = Regex.Replace(xml, @"\s+(</(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)>)", "$1 ");
			}

			private void FormatOpenTags()
			{
				xml = Regex.Replace(xml, @"(<(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)([^>]*)>)\s+", " $1");
			}

			private void FormatPunctuation()
			{
				// Format brakets
				xml = Regex.Replace(xml, @"(\s*)(\()(\s+)", "$1$3$2");
				xml = Regex.Replace(xml, @"(\s+)(\))(\s*)", "$3$1$2");
				xml = Regex.Replace(xml, @"(\)|\])\s+(\)|\])", "$1$2");
				xml = Regex.Replace(xml, @"(\(|\[)\s+(\(|\[)", "$1$2");
				// Format other punctuation
				xml = Regex.Replace(xml, @"(\s+)([,;\.])", "$2$1");
				xml = Regex.Replace(xml, @"([,\.\;])(<i>|<b>)", "$1 $2");
			}

			private void FormatPageBreaks()
			{
				xml = Regex.Replace(xml, @"(<!--PageBreak-->)(\s+)(<!--PageBreak-->)", "$1$3$2");
				xml = Regex.Replace(xml, @"(\s*)(<p>|<tp:nomenclature-citation>|<title>|<label>)\s*((<!--PageBreak-->)+)\s*", "$1$3$1$2");

				xml = Regex.Replace(xml, @"<tr[^>]*>\s*<(td|th)[^>]*>\s*((<!--PageBreak-->)+)\s*</(td|th)>\s*</tr>", "$2");
				xml = Regex.Replace(xml, @"<tr>\s*((<!--PageBreak-->)+)\s*</tr>", "$1");
				xml = Regex.Replace(xml, @"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1");
				xml = Regex.Replace(xml, @"(<i>[^<>]*?)((<!--PageBreak-->)+)", "$2$1");

				xml = Regex.Replace(xml, @"(\s*)(<kwd>.*?)\s*((<!--PageBreak-->)+)(.*</kwd>)", "$1$2$5$1$3");
				xml = Regex.Replace(xml, @"<(title|label|kwd|p)>\s*((<!--PageBreak-->)+)</(title|label|kwd|p)>", "$2");

				xml = Regex.Replace(xml, @"(<b>|<i>)((<!--PageBreak-->)+)(</b>|</i>)", "$2");
				xml = Regex.Replace(xml, @"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");

				xml = Regex.Replace(xml, @"(<!--PageBreak-->)\s+(<!--PageBreak-->)", "$1$2");
			}

			private void RemoveEmptyTags()
			{
				xml = Regex.Replace(xml, @"<p>\s*</p>|<xref-group>\s*</xref-group>|<i></i>|<i\s*/>|<sup\s*/>|<sub\s*/>|<b\s*/>|<label\s*/>|<b></b>|<kwd>\s*</kwd>|<sup></sup>|<sub></sub>|<mixed-citation [^>]*>\s*</mixed-citation>|<tp:nomenclature-citation>\s*</tp:nomenclature-citation>|<ref [^>]*>\s*</ref>|<source></source>|<u></u>|<u\s*/>|<monospace></monospace>|<monospace\s*/>", "");
				xml = Regex.Replace(xml, @"<i>\s+</i>|<b>\s+</b>|<sup>\s+</sup>|<sub>\s+</sub>|<source>\s+</source>|<u>\s+</u>|<monospace>\s+</monospace>", " ");
			}

			private void FormatReferances()
			{
				xml = Regex.Replace(xml, @"(\S)(<article-title>|<source>)", "$1 $2");
				xml = Regex.Replace(xml, @"(</article-title>|</source>)(\S)", "$1 $2");
				xml = Regex.Replace(xml, @"(?<=</surname>)\s*(?=<given-names>)", " ");

				xml = Regex.Replace(xml, @"</volume>\s+\(<issue>", "</volume>(<issue>");
				xml = Regex.Replace(xml, @"</issue>(\))+", "</issue>)");

				//xml = Regex.Replace(xml, @"<role>Ed\.</role>", "<role>Ed</role>");
				xml = Regex.Replace(xml, @"</role>\)(\.|,|;|:)", "</role>)");
			}
		}
	}

	namespace NlmSystem
	{
		public class Format : Base
		{
			public Format(string xml)
			{
				this.xml = xml;
			}

			public Format()
			{
				this.xml = "";
			}

			public void BoldItalicSpaces()
			{
				// Format blank spaces
				xml = Regex.Replace(xml, "(\\s+)(</i>|</b>|</u>)", "$2$1");
				xml = Regex.Replace(xml, "(<b>|<i>|<u>)(\\s+)", "$2$1");
				// Remove sequental tags
				xml = Regex.Replace(xml, @"</i>(\s*)<i>", "$1");
				xml = Regex.Replace(xml, @"</b>(\s*)<b>", "$1");
				xml = Regex.Replace(xml, @"<i>(\s*)</i>", "$1");
				xml = Regex.Replace(xml, @"<b>(\s*)</b>", "$1");
				xml = Regex.Replace(xml, @"<sup>(\s*)</sup>", "$1");
				xml = Regex.Replace(xml, @"<sub>(\s*)</sub>", "$1");
				xml = Regex.Replace(xml, @"(</i>|</b>)(\w)", "$1 $2");
				xml = Regex.Replace(xml, @"(</i>)(<b>)", "$1 $2");
				xml = Regex.Replace(xml, @"(</b>)(<i>)", "$1 $2");
				xml = Regex.Replace(xml, @"(“|‘)(\s+)(<i>)", "$2$1$3");
				xml = Regex.Replace(xml, @"(\s*’\s*)(</i>)", "$2$1");
				xml = Regex.Replace(xml, @"(<i>)(\s*‘\s*)", "$2$1");
			}

			public void BoldItalic()
			{
				BoldItalicSpaces();
				xml = Regex.Replace(xml, @"(\s*\(\s*)(</i>|</b>)", "$2 $1");
				xml = Regex.Replace(xml, @"(<i>|<b>)(\s*\)s*)", "$2 $1");
				xml = Regex.Replace(xml, @"(</i>)(\()", "$1 $2");
				xml = Regex.Replace(xml, @"(?<!\&[a-z]+)([,;:]+)(</i>)", "$2$1");
				xml = Regex.Replace(xml, @"(<i>|<b>)([\.,;:]+)", "$2 $1");
				xml = Regex.Replace(xml, @"([,\.;])(<i>|<b>)", "$1 $2");
				xml = Regex.Replace(xml, @"(</i>|</b>)\s+([,\.;])", "$1$2 ");
				xml = Regex.Replace(xml, @"(<b>|<i>)([,\.;\-–])(</b>|</i>)", "$2");
				xml = Regex.Replace(xml, @"(<i>)([A-Z][a-z]{0,2}|[a-z]{0,3})(</i>)(\.)", "$1$2$4$3");

				// Genus + (Subgenus)
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");
				// Genus + (Subgenus) + species
				xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+\s\([A-Z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");
				// sensu lato & sensu stricto
				xml = Regex.Replace(xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
				xml = Regex.Replace(xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
				xml = Regex.Replace(xml, @"<i>(s(ensu|\.))\s*(l|s)</i>\.", "<i>$1 $3.</i>");

				// Remove empty tags
				xml = Regex.Replace(xml, @"(<i></i>|<b></b>|<sup></sup>|<sub></sub>|<u></u>|<i\s*/>|<b\*/>|<sup\s*/>|<sub\s*/>)", "");

				// Remove bold and italic around punctuation
				xml = Regex.Replace(xml, @"<(b|i)>([,;\.\-\:\s–]+)</(b|i)>", "$2");
				for (int i = 0; i < 6; i++)
				{
					// Genus[ species[ subspecies]]
					xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>", "<i>$1</i>$3<i>$4</i>");
					// Genus (Subgenus)[ species[ subspecies]]
					xml = Regex.Replace(xml, @"<i>([A-Z][a-z\.]+\s*\(\s*[A-Z][a-z\.]+\s*\)([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>", "<i>$1</i>$3<i>$4</i>");
				}
				xml = Regex.Replace(xml, "</p>\\s*<p", "</p>\n<p");

				xml = Regex.Replace(xml, @"([A-Z][a-z\.-]+)\s+(s+p+)</i>\.", "$1</i> $2.");
			}

			public void ClearTagsFromTags()
			{
				xml = Regex.Replace(xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
			}

			public void FormatCloseTags()
			{
				xml = Regex.Replace(xml, @"(\s+)(</b>|</i>|</u>)", "$2$1");
				xml = Regex.Replace(xml, @"\s+(</value>|</p>|</th>|</td>)", "$1");
			}

			public void FormatOpenTags()
			{
				xml = Regex.Replace(xml, @"(<(value|td|th)(\s+[^>]*>|>))\s+", "$1");
				xml = Regex.Replace(xml, @"(<(b|i|u|sub|sup)(\s+[^>]*>|>))(\s+)", "$4$1");
			}

			public void FormatPunctuation()
			{
				// Format brakets
				xml = Regex.Replace(xml, @"([\(\[])(\s+)", "$2$1");
				xml = Regex.Replace(xml, @"(\s+)([\)\]])", "$2$1");
				xml = Regex.Replace(xml, @"([\)\]])(\s+)([\)\]])", "$1$3$2");
				xml = Regex.Replace(xml, @"([\(\[])(\s+)([\(\[])", "$2$1$3");
				// Format other punctuation
				xml = Regex.Replace(xml, @"(\s+)([,;\.])", "$2$1");
				xml = Regex.Replace(xml, @"([,\.\;])(<i>|<b>)", "$1 $2");
			}

			public void RemoveEmptyTags()
			{
				xml = Regex.Replace(xml, @"<(i|b|u|sup|sub)(\s+[^>]*>|>)(\s*)</(i|b|u|sup|sub)>", "$3");
				xml = Regex.Replace(xml, @"<i\s*/>|<b\s*/>|<u\s*/>|<sup\s*/>|<sub\s*/>", "");
			}

			public void InitialFormat()
			{
				FormatCloseTags();
				FormatOpenTags();
				BoldItalic();

				for (Match m = Regex.Match(xml, "<p>[\\S\\s]*?</p>"); m.Success; m = m.NextMatch())
				{
					string replace = Regex.Replace(m.Value, "\n", " ");
					xml = Regex.Replace(xml, Regex.Escape(m.Value), replace);
				}

				// male and female
				xml = Regex.Replace(xml, "<i>([♂♀\\s]+)</i>", "$1");

				// Post-formatting
				for (int i = 0; i < 3; i++)
				{
					BoldItalic();
					FormatPunctuation();
					RemoveEmptyTags();
					FormatCloseTags();
					FormatOpenTags();
				}
				xml = Regex.Replace(xml, @"<\s+/", "</");

				// Remove empty lines
				xml = Regex.Replace(xml, @"\n\s*\n", "\n");
				// sensu lato & stricto
				xml = Regex.Replace(xml, @"(<i>)((s\.|sens?u?)\s+[sl][a-z]*)(</i>)\.", "$1$2.$4");
				//
				xml = Regex.Replace(xml, @"<\s+/", "</");
			}
		}
	}
}
