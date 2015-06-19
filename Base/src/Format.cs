using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base.Format
{
    public class Format
    {
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

    namespace Nlm
    {
        public class Format : Base
        {
            public Format() : base() { }
            public Format(string xml) : base(xml) { }

            public void InitialFormat()
            {
                xml = Regex.Replace(xml, "\r", "");
                xml = Regex.Replace(xml, "<bold-italic>", "<bold><italic>");
                xml = Regex.Replace(xml, "</bold-italic>", "</italic></bold>");
                xml = Regex.Replace(xml, "</?fake_tag>", "");
                xml = Regex.Replace(xml, "tp:taxon-name", "tp:Taxon-name");

                xml = Regex.Replace(xml, @"(?<=<table id="".*?"")[^>]*(?=>)", "");
                //xml = Regex.Replace(xml, "( headerRowCount=\"\\d+\"| bodyRowCount=\"\\d+\"| columnCount=\"\\d+\"| aid:table=\"[a-z]+\"| aid:trows=\"\\d+\"| aid:tcols=\"\\d+\")", "");

                // Remove white spaces in @id and @rid values
                xml = Regex.Replace(xml, @"(?<=<[^>]+\br?id="")\s*([^<>""]*?)\s*(?="")", "$1");

                //Alert.Message("Break 1");

                ParseXmlStringToXmlDocument();
                foreach (XmlNode node in xmlDocument.SelectNodes("//title|//label|//article-title|//th", namespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?bold>|</?b>", "");
                }
                foreach (XmlNode node in xmlDocument.SelectNodes("//title|//label|//article-title|//th|//td|//p|//kwd|//ref|//tp:Taxon-name", namespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "^\\s+|\\s+$", "");
                    node.InnerXml = Regex.Replace(node.InnerXml, "\\s+", " ");
                }
                foreach (XmlNode node in xmlDocument.SelectNodes("//table-wrap[table[@id]]", namespaceManager))
                {
                    node.Attributes.Prepend(node["table"].Attributes["id"]);
                }
                foreach (XmlNode node in xmlDocument.SelectNodes("//table[not(@rules)]", namespaceManager))
                {
                    XmlAttribute rules = xmlDocument.CreateAttribute("rules");
                    rules.AppendChild(xmlDocument.CreateTextNode("all"));
                    node.Attributes.Append(rules);
                }
                xml = xmlDocument.OuterXml;

                //Alert.Message("Break 2");

                // Format sec-type attribute
                xml = Regex.Replace(xml, "(sec-type=\".*?)[,\\.;:](\")", "$1$2");

                // Format openeing and closing tags
                FormatCloseTags();
                FormatOpenTags();
                BoldItalic();

                //Alert.Message("Break 3");

                // Format wrong figures' labels
                xml = Regex.Replace(xml, @"(\s*)(<caption>)\s*(<p>)\s*<bold>\s*((Figure|Table).*?)\s*</bold>", "$1<label>$4</label>$1$2$3");

                //Alert.Message("Break 4");

                FormatReferances();

                //Alert.Message("Break 5 Referances");

                FormatPageBreaks();

                //Alert.Message("Break 6 PageBreaks");

                // male and female
                xml = Regex.Replace(xml, "<italic>([♂♀\\s]+)</italic>", "$1");

                // Post-formatting
                for (int i = 0; i < 3; i++)
                {
                    BoldItalic();
                    //Alert.Message("= 1");
                    FormatPunctuation();
                    //Alert.Message("= 2");
                    //FormatPageBreaks();
                    //Alert.Message("= 3");
                    RemoveEmptyTags();
                    //Alert.Message("= 4");
                    FormatCloseTags();
                    //Alert.Message("= 5");
                    FormatOpenTags();
                    //Alert.Message("= 6");
                    //Alert.Message(i);
                }

                //Alert.Message("Break 7");

                // Subspecies, subgenera, etc.
                xml = Regex.Replace(xml, @"<italic>([A-Z][a-z]+)</italic>\.\s*<italic>([a-z]+)</italic>", "<italic>$1. $2</italic>");
                xml = Regex.Replace(xml, @"<italic>([A-Z][a-z]*\.\s+[a-z]+)</italic>\.\s*<italic>([a-z]+)</italic>", "<italic>$1. $2</italic>");
                xml = Regex.Replace(xml, @"(<italic>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</italic>)(\))", "$1$3$2");
                xml = Regex.Replace(xml, @"</italic>\s+<italic>", " ");
                xml = Regex.Replace(xml, @"</italic><italic>", "");

                // Clear empty symbols out of <article> tag
                xml = Regex.Replace(xml, @"(?<=</article>)\s+", "");
                //
                xml = Regex.Replace(xml, @"<\s+/", "</");

                // Supplementary materials external link
                xml = Regex.Replace(xml, "(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2");

                // Misc
                xml = Regex.Replace(xml, @"\s*(<\w[^>]*>\s*)?&lt;br\s*/&gt;(\s*</\w[^>]*>)?\s*(?=</p>\s*</caption>)", "");
                xml = Regex.Replace(xml, @"\.</italic>(?=</p>)", "</italic>.");

                ParseXmlStringToXmlDocument();
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


                // Remove some empty tags
                xml = Regex.Replace(xml, @"<source>\s*</source>", "");
                // Remove empty lines
                xml = Regex.Replace(xml, @"\n\s*\n", "\n");
                // sensu lato & stricto
                xml = Regex.Replace(xml, @"(<italic>)((s\.|sens?u?)\s+[sl][a-z]*)(</italic>)\.", "$1$2.$4");
                //
                xml = Regex.Replace(xml, @"<\s+/", "</");
                //
                FormatPageBreaks();

                xml = Regex.Replace(xml, "(\\s+)(<tp:nomenclature-citation-list>)(<tp:nomenclature-citation>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<ref-list>)(<title>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<publisher>)(<publisher-name>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<pub-date [^>]*>)(<)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<permissions>)(<copyright-statement>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<permissions>)(<license)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<kwd-group>)(<label>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, "(\\s+)(<title-group>)(<article-title>)", "$1$2$1    $3");
                xml = Regex.Replace(xml, @"(\s*)(</table>)\s*&lt;br\s*/&gt;\s*", "$1$2$1");

                xml = Regex.Replace(xml, "\n\\s*(?=\n)", "");

                xml = Regex.Replace(xml, @"(?<=(\s*)<license[^>]*>)(<license-p>)", "$1    $2");
            }

            public void SubgenusBrackets()
            {
                xml = Regex.Replace(xml, "\\((<tp:taxon-name-part taxon-name-part-type=\"subgenus\">)([A-Z][a-z\\.]+)(</tp:taxon-name-part>)\\)", "$1 ($2) $3");
            }

            public void BoldItalicSpaces()
            {
                xml = Regex.Replace(xml, @"(<bold>|<italic>)(\s+)", " $1");
                xml = Regex.Replace(xml, @"\s+(</bold>|</italic>)", "$1 ");
                xml = Regex.Replace(xml, @"(</bold>\s+<bold>|</italic>\s+<italic>|<bold>\s+</bold>|<italic>\s+</italic>)", " ");
                xml = Regex.Replace(xml, @"(</bold><bold>|<bold></bold>|</italic><italic>|<italic></italic>)", "");
                xml = Regex.Replace(xml, @"(</italic>|</bold>)(\w)", "$1 $2");
                //xml = Regex.Replace(xml, @"(“|‘)\s+(<italic>|<bold>)", " $1$2");
                //xml = Regex.Replace(xml, @"(</italic>|</bold>)\s+(’)", "$1$2 ");

                xml = Regex.Replace(xml, @"([\(\[])\s+(<italic>|<bold>|<underline>|<sub>|<sup>)\s*", " $1$2");
                xml = Regex.Replace(xml, @"\s+(</italic>|</bold>|</underline>|</sub>|</sup>)([^,;\)\]\.])", "$1 $2");

                xml = Regex.Replace(xml, @"(</bold>|</italic>)\s*(<bold>|<italic>)", "$1 $2");

                xml = Regex.Replace(xml, @"([,\.;])(<italic>|<bold>)", "$1 $2");
                xml = Regex.Replace(xml, @"(</italic>|</bold>)\s+([,\.;])", "$1$2 ");
            }

            public void BoldItalic()
            {
                BoldItalicSpaces();

                xml = Regex.Replace(xml, @"\&lt;\s*br\s*/\s*\&gt;</(italic|bold)>", "</$1>&lt;br/&gt;");
                xml = Regex.Replace(xml, @"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</italic>|</bold>)", "$2$1");
                xml = Regex.Replace(xml, @"(<italic>|<bold>)([^\w<>\.\(\)\&]+)", "$2$1");
                xml = Regex.Replace(xml, @"(<italic>)([A-Za-z][a-z]{0,2})(</italic>)(\.)", "$1$2$4$3");
                //xml = Regex.Replace(xml, @"(</bold>)([\.:])", "$2$1");

                xml = Regex.Replace(xml, @"\s*\(\s*(</italic>|</bold>)", "$1 (");
                xml = Regex.Replace(xml, @"(<italic>|<bold>)\s*\)\s*", ") $1");

                //xml = Regex.Replace(xml, "(’)(</italic>)", "$2$1");
                //xml = Regex.Replace(xml, "(<italic>)(‘)", "$2$1");

                xml = Regex.Replace(xml, @"(<bold>|<italic>)([,\s\.:;\-––])(</bold>|</italic>)", "$2");

                // Genus + (Subgenus)
                xml = Regex.Replace(xml, @"<italic>([A-Z][a-z\.]+)</italic>\s*\(\s*<italic>([A-Za-z][a-z\.]+)</italic>\s*\)", "<italic>$1 ($2)</italic>");
                // Genus + (Subgenus) + species
                xml = Regex.Replace(xml, @"<italic>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</italic>\s*<italic>([a-z\.\-]+)</italic>", "<italic>$1 $2</italic>");
                // sensu lato & sensu stricto
                xml = Regex.Replace(xml, @"<italic>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*.*?)</italic>", "<italic>$1</italic> <italic>$2</italic>");
                xml = Regex.Replace(xml, @"<italic>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</italic>", "<italic>$1</italic> <italic>$2</italic>");
                xml = Regex.Replace(xml, @"<italic>(s(ensu|\.))\s*(l|s|str)</italic>\.", "<italic>$1 $3.</italic>");

                // Remove empty tags
                xml = Regex.Replace(xml, "(<italic></italic>|<bold></bold>|<source></source>|<sup></sup>)", "");
                // Paste some intervals
                xml = Regex.Replace(xml, "</italic><bold>", "</italic> <bold>");

                xml = Regex.Replace(xml, @"(</italic>)(\()", "$1 $2");
                xml = Regex.Replace(xml, @"(<italic>)([\.,;:\s]+)", "$2$1");
            }

            public void FormatCloseTags()
            {
                xml = Regex.Replace(xml, @"\s+(</(source|issue-title|bold|italic|underline|monospace|year|month|day|volume|fpage|lpage)>)", "$1 ");
                xml = Regex.Replace(xml, @"\s+(</(label|title|p|license-p|li|attrib|mixed-citation|object-id|xref|xref-group|kwd|td|ref|caption|th|tp:nomenclature-citation|article-title|self-uri|name|given-names|surname|person-group|graphic)>)", "$1");
            }

            public void FormatOpenTags()
            {
                xml = Regex.Replace(xml, @"(<(source|issue-title|bold|italic|underline|monospace|year|month|day|volume|fpage|lpage)([^>]*)>)\s+", " $1");
                xml = Regex.Replace(xml, @"(<(label|title|p|license-p|li|attrib|mixed-citation|object-id|xref|xref-group|kwd|td|ref|caption|th|tp:nomenclature-citation|article-title|self-uri|name|given-names|surname|person-group|graphic)([^>]*)>)\s+", "$1");
            }

            public void FormatPunctuation()
            {
                // Format brakets
                xml = Regex.Replace(xml, @"(\s*)(\()(\s+)", "$1$3$2");
                xml = Regex.Replace(xml, @"(\s+)(\))(\s*)", "$3$1$2");
                xml = Regex.Replace(xml, @"(\)|\])\s+(\)|\])", "$1$2");
                xml = Regex.Replace(xml, @"(\(|\[)\s+(\(|\[)", "$1$2");
                // Format other punctuation
                xml = Regex.Replace(xml, @"(\s+)([,;\.])", "$2$1");
                xml = Regex.Replace(xml, @"([,\.\;])(<italic>|<bold>)", "$1 $2");
            }

            public void FormatPageBreaks()
            {
                xml = Regex.Replace(xml, @"(<!--PageBreak-->)\s+(<!--PageBreak-->)", "$1$2");
                xml = Regex.Replace(xml, @"(\s*)(<p>|<tp:nomenclature-citation>|<title>|<label>)\s*((<!--PageBreak-->)+)\s*", "$1$3$1$2");

                xml = Regex.Replace(xml, @"<tr[^>]*>\s*<(td|th)[^>]*>\s*((<!--PageBreak-->)+)\s*</(td|th)>\s*</tr>", "$2");
                xml = Regex.Replace(xml, @"<tr>\s*((<!--PageBreak-->)+)\s*</tr>", "$1");
                xml = Regex.Replace(xml, @"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1");
                xml = Regex.Replace(xml, @"(<italic>.*?)((<!--PageBreak-->)+)", "$2$1");

                xml = Regex.Replace(xml, @"(\s*)(<kwd>.*?)\s*((<!--PageBreak-->)+)(.*</kwd>)", "$1$2$5$1$3");
                xml = Regex.Replace(xml, @"<(title|label|kwd|p)>\s*((<!--PageBreak-->)+)</(title|label|kwd|p)>", "$2");

                xml = Regex.Replace(xml, @"(<bold>|<italic>)((<!--PageBreak-->)+)(</bold>|</italic>)", "$2");
                xml = Regex.Replace(xml, @"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");
                xml = Regex.Replace(xml, @"\s*<xref-group>\s*</xref-group>", "");
            }

            public void RemoveEmptyTags()
            {
                xml = Regex.Replace(xml, @"(<p>\s*</p>)|(<italic></italic>)|<italic\s*/>|<sup\s*/>|<sub\s*/>|<bold\s*/>|<label\s*/>|(<bold></bold>)|(<kwd>\s*</kwd>)|(<sup></sup>)|(<sub></sub>)|(<mixed-citation [^>]*>\s*</mixed-citation>)|(<tp:nomenclature-citation>\s*</tp:nomenclature-citation>)|(<ref [^>]*>\s*</ref>)|(<source></source>)|(<underline></underline>)|<underline\s*/>|(<monospace></monospace>)|<monospace\s*/>", "");
                xml = Regex.Replace(xml, @"(<italic>\s+</italic>)|(<bold>\s+</bold>)|(<sup>\s+</sup>)|(<sub>\s+</sub>)|(<source>\s+</source>)|(<underline>\s+</underline>)|(<monospace>\s+</monospace>)", " ");
            }

            public void FormatReferances()
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
