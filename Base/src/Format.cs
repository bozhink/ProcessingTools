using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base.Format
{
    namespace Nlm
    {
        public class Formatter : Base
        {
            public Formatter()
                : base()
            {
            }

            public Formatter(string xml)
                : base(xml)
            {
            }

            public Formatter(Config config)
                : base(config)
            {
            }

            public Formatter(Config config, string xml)
                : base(config, xml)
            {
            }

            public Formatter(Base baseObject)
                : base(baseObject)
            {
            }

            public void InitialFormat()
            {
                this.xml = Regex.Replace(this.xml, "\r", string.Empty);

                // Remove white spaces in @id and @rid values
                this.xml = Regex.Replace(this.xml, @"(?<=<[^>]+\br?id="")\s*([^<>""]*?)\s*(?="")", "$1");

                // Format openeing and closing tags
                this.FormatCloseTags();
                this.FormatOpenTags();
                this.BoldItalic();

                // Format wrong figures' labels
                this.xml = Regex.Replace(this.xml, @"<caption>\s+<p>", "<caption><p>");
                this.xml = Regex.Replace(this.xml, @"</p>\s+</caption>", "</p></caption>");
                this.xml = Regex.Replace(this.xml, @"(\s*)(<caption><p>)\s*<b>\s*((Figure|Map|Plate|Table|Suppl|Box)[^<>]*?)\s*</b>", "$1<label>$3</label>$1$2");

                this.FormatReferances();
                this.FormatPageBreaks();

                // male and female
                this.xml = Regex.Replace(this.xml, "<i>([♂♀\\s]+)</i>", "$1");

                // Post-formatting
                for (int i = 0; i < 3; i++)
                {
                    this.BoldItalic();
                    this.FormatPunctuation();
                    ////FormatPageBreaks();
                    this.RemoveEmptyTags();
                    this.FormatCloseTags();
                    this.FormatOpenTags();
                }

                this.ParseXmlStringToXmlDocument();
                foreach (XmlNode node in this.xmlDocument.SelectNodes("//title|//label|//article-title|//th|//td|//p|//license-p|//li|//attrib|//kwd|//ref|//mixed-citation|//object-id|//xref-group|//tp:nomenclature-citation|//self-uri|//name|//given-names|//surname|//person-group|//graphic[string()!='']", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, @"\A\s+|\s+\Z", string.Empty);
                    node.InnerXml = Regex.Replace(node.InnerXml, @"\s+", " ");
                }

                foreach (XmlNode node in this.xmlDocument.SelectNodes("//td|//th", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "&lt;br/&gt;", "<break />");
                }

                foreach (XmlNode node in this.xmlDocument.SelectNodes("//front//contrib", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(
                        node.InnerXml,
                        @"(\s*)(<name [^>]*>)\s*(<surname>.*?</surname>)\s*(<given-names>.*?</given-names>)\s*(</name>)",
                        "$1$2$1    $3$1    $4$1$5");
                }

                this.xml = this.xmlDocument.OuterXml;

                // Subspecies, subgenera, etc.
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z]*\.\s+[a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
                this.xml = Regex.Replace(this.xml, @"(<i>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</i>)(\))", "$1$3$2");
                this.xml = Regex.Replace(this.xml, @"</i>\s+<i>", " ");
                this.xml = Regex.Replace(this.xml, @"</i><i>", string.Empty);

                // Clear empty symbols out of <article> tag
                this.xml = Regex.Replace(this.xml, @"(?<=</article>)\s+", string.Empty);

                // Supplementary materials external link
                this.xml = Regex.Replace(this.xml, "(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2");

                // Misc
                this.xml = Regex.Replace(this.xml, @"\s*(<\w[^>]*>\s*)?&lt;br\s*/&gt;(\s*</\w[^>]*>)?\s*(?=</p>\s*</caption>)", string.Empty);
                this.xml = Regex.Replace(this.xml, @"\.</i>(?=</p>)", "</i>.");

                // sensu lato & stricto
                this.xml = Regex.Replace(this.xml, @"(<i>)((s\.|sens?u?)\s+[sl][a-z]*)(</i>)\.", "$1$2.$4");

                this.xml = Regex.Replace(this.xml, @"<\s+/", "</");

                this.RemoveEmptyTags();
                this.FormatPageBreaks();

                this.xml = Regex.Replace(this.xml, "(\\s+)(<tp:nomenclature-citation-list>)(<tp:nomenclature-citation>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<ref-list>)(<title>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<publisher>)(<publisher-name>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<pub-date [^>]*>)(<)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<permissions>)(<copyright-statement>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<permissions>)(<license)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, @"(?<=(\s*)<license[^>]*>)(<license-p>)", "$1    $2");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<kwd-group>)(<label>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, "(\\s+)(<title-group>)(<article-title>)", "$1$2$1    $3");
                this.xml = Regex.Replace(this.xml, @"(\s*)(</table>)\s*&lt;br\s*/&gt;\s*", "$1$2$1");

                // Remove empty lines
                this.xml = Regex.Replace(this.xml, "\n\\s*(?=\n)", string.Empty);
            }

            private void BoldItalicSpaces()
            {
                this.xml = Regex.Replace(this.xml, @"(<b>|<i>)(\s+)", " $1");
                this.xml = Regex.Replace(this.xml, @"\s+(</b>|</i>)", "$1 ");
                this.xml = Regex.Replace(this.xml, @"(</b>\s+<b>|</i>\s+<i>|<b>\s+</b>|<i>\s+</i>)", " ");
                this.xml = Regex.Replace(this.xml, @"(</b><b>|<b></b>|</i><i>|<i></i>)", string.Empty);
                this.xml = Regex.Replace(this.xml, @"(</italic>|</bold>)(\w)", "$1 $2");
                ////xml = Regex.Replace(xml, @"(“|‘)\s+(<i>|<b>)", " $1$2");
                ////xml = Regex.Replace(xml, @"(</i>|</b>)\s+(’)", "$1$2 ");

                this.xml = Regex.Replace(this.xml, @"([\(\[])\s+(<i>|<b>|<u>|<sub>|<sup>)\s*", " $1$2");
                ////xml = Regex.Replace(xml, @"\s+(</i>|</b>|</u>|</sub>|</sup>)([^,;\)\]\.])", "$1 $2");

                ////xml = Regex.Replace(xml, @"(</b>|</i>)\s*(<b>|<i>)", "$1 $2");

                this.xml = Regex.Replace(this.xml, @"([,\.;])(<i>|<b>)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(</i>|</b>)\s+([,\.;])", "$1$2 ");
            }

            private void BoldItalic()
            {
                this.BoldItalicSpaces();

                this.xml = Regex.Replace(this.xml, @"\&lt;\s*br\s*/\s*\&gt;(</i>|</b>)", "$1&lt;br/&gt;");
                this.xml = Regex.Replace(this.xml, @"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</i>|</b>)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"(<i>|<b>)([^\w<>\.\(\)\&]+)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"(<i>)([A-Za-z][a-z]{0,2})(</i>)(\.)", "$1$2$4$3");
                ////xml = Regex.Replace(xml, @"(</bold>)([\.:])", "$2$1");

                this.xml = Regex.Replace(this.xml, @"\s*\(\s*(</i>|</b>)", "$1 (");
                this.xml = Regex.Replace(this.xml, @"(<i>|<b>)\s*\)\s*", ") $1");

                ////xml = Regex.Replace(xml, "(’)(</italic>)", "$2$1");
                ////xml = Regex.Replace(xml, "(<italic>)(‘)", "$2$1");

                this.xml = Regex.Replace(this.xml, @"(<b>|<i>)([,\s\.:;\-––])(</b>|</i>)", "$2");

                // Genus + (Subgenus)
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Za-z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");

                // Genus + (Subgenus) + species
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");

                // sensu lato & sensu stricto
                this.xml = Regex.Replace(this.xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*.*?)</i>", "<i>$1</i> <i>$2</i>");
                this.xml = Regex.Replace(this.xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
                this.xml = Regex.Replace(this.xml, @"<i>(s(ensu|\.))\s*(l|s|str)</i>\.", "<i>$1 $3.</i>");

                // Paste some intervals
                ////xml = Regex.Replace(xml, "</i><b>", "</i> <b>");

                this.xml = Regex.Replace(this.xml, @"(</i>)(\()", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(<i>)([\.,;:\s]+)", "$2$1");
            }

            private void FormatCloseTags()
            {
                this.xml = Regex.Replace(this.xml, @"\s+(</(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)>)", "$1 ");
            }

            private void FormatOpenTags()
            {
                this.xml = Regex.Replace(this.xml, @"(<(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)([^>]*)>)\s+", " $1");
            }

            private void FormatPunctuation()
            {
                // Format brakets
                this.xml = Regex.Replace(this.xml, @"(\s*)(\()(\s+)", "$1$3$2");
                this.xml = Regex.Replace(this.xml, @"(\s+)(\))(\s*)", "$3$1$2");
                this.xml = Regex.Replace(this.xml, @"(\)|\])\s+(\)|\])", "$1$2");
                this.xml = Regex.Replace(this.xml, @"(\(|\[)\s+(\(|\[)", "$1$2");

                // Format other punctuation
                this.xml = Regex.Replace(this.xml, @"(\s+)([,;\.])", "$2$1");
                this.xml = Regex.Replace(this.xml, @"([,\.\;])(<i>|<b>)", "$1 $2");
            }

            private void FormatPageBreaks()
            {
                this.xml = Regex.Replace(this.xml, @"(<!--PageBreak-->)(\s+)(<!--PageBreak-->)", "$1$3$2");
                this.xml = Regex.Replace(this.xml, @"(\s*)(<p>|<tp:nomenclature-citation>|<title>|<label>)\s*((<!--PageBreak-->)+)\s*", "$1$3$1$2");

                this.xml = Regex.Replace(this.xml, @"<tr[^>]*>\s*<(td|th)[^>]*>\s*((<!--PageBreak-->)+)\s*</(td|th)>\s*</tr>", "$2");
                this.xml = Regex.Replace(this.xml, @"<tr>\s*((<!--PageBreak-->)+)\s*</tr>", "$1");
                this.xml = Regex.Replace(this.xml, @"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1");
                this.xml = Regex.Replace(this.xml, @"(<i>[^<>]*?)((<!--PageBreak-->)+)", "$2$1");

                this.xml = Regex.Replace(this.xml, @"(\s*)(<kwd>.*?)\s*((<!--PageBreak-->)+)(.*</kwd>)", "$1$2$5$1$3");
                this.xml = Regex.Replace(this.xml, @"<(title|label|kwd|p)>\s*((<!--PageBreak-->)+)</(title|label|kwd|p)>", "$2");

                this.xml = Regex.Replace(this.xml, @"(<b>|<i>)((<!--PageBreak-->)+)(</b>|</i>)", "$2");
                this.xml = Regex.Replace(this.xml, @"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");

                this.xml = Regex.Replace(this.xml, @"(<!--PageBreak-->)\s+(<!--PageBreak-->)", "$1$2");
            }

            private void RemoveEmptyTags()
            {
                this.xml = Regex.Replace(
                    this.xml,
                    @"<p>\s*</p>|<xref-group>\s*</xref-group>|<i></i>|<i\s*/>|<sup\s*/>|<sub\s*/>|<b\s*/>|<label\s*/>|<b></b>|<kwd>\s*</kwd>|<sup></sup>|<sub></sub>|<mixed-citation [^>]*>\s*</mixed-citation>|<tp:nomenclature-citation>\s*</tp:nomenclature-citation>|<ref [^>]*>\s*</ref>|<source></source>|<u></u>|<u\s*/>|<monospace></monospace>|<monospace\s*/>",
                    string.Empty);

                this.xml = Regex.Replace(this.xml, @"<i>\s+</i>|<b>\s+</b>|<sup>\s+</sup>|<sub>\s+</sub>|<source>\s+</source>|<u>\s+</u>|<monospace>\s+</monospace>", " ");
            }

            private void FormatReferances()
            {
                this.xml = Regex.Replace(this.xml, @"(\S)(<article-title>|<source>)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(</article-title>|</source>)(\S)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(?<=</surname>)\s*(?=<given-names>)", " ");

                this.xml = Regex.Replace(this.xml, @"</volume>\s+\(<issue>", "</volume>(<issue>");
                this.xml = Regex.Replace(this.xml, @"</issue>(\))+", "</issue>)");

                ////xml = Regex.Replace(xml, @"<role>Ed\.</role>", "<role>Ed</role>");
                this.xml = Regex.Replace(this.xml, @"</role>\)(\.|,|;|:)", "</role>)");
            }
        }
    }

    namespace NlmSystem
    {
        public class Formatter : Base
        {
            public Formatter()
                : base()
            {
            }

            public Formatter(string xml)
                : base(xml)
            {
            }

            public Formatter(Config config)
                : base(config)
            {
            }

            public Formatter(Config config, string xml)
                : base(config, xml)
            {
            }

            public Formatter(Base baseObject)
                : base(baseObject)
            {
            }

            public void InitialFormat()
            {
                this.FormatCloseTags();
                this.FormatOpenTags();
                this.BoldItalic();

                for (Match m = Regex.Match(this.xml, "<p>[\\S\\s]*?</p>"); m.Success; m = m.NextMatch())
                {
                    string replace = Regex.Replace(m.Value, "\n", " ");
                    this.xml = Regex.Replace(this.xml, Regex.Escape(m.Value), replace);
                }

                // male and female
                this.xml = Regex.Replace(this.xml, "<i>([♂♀\\s]+)</i>", "$1");

                // Post-formatting
                for (int i = 0; i < 3; i++)
                {
                    this.BoldItalic();
                    this.FormatPunctuation();
                    this.RemoveEmptyTags();
                    this.FormatCloseTags();
                    this.FormatOpenTags();
                }

                this.xml = Regex.Replace(this.xml, @"<\s+/", "</");

                // Remove empty lines
                this.xml = Regex.Replace(this.xml, @"\n\s*\n", "\n");

                // sensu lato & stricto
                this.xml = Regex.Replace(this.xml, @"(<i>)((s\.|sens?u?)\s+[sl][a-z]*)(</i>)\.", "$1$2.$4");

                this.xml = Regex.Replace(this.xml, @"<\s+/", "</");
            }

            private void BoldItalicSpaces()
            {
                // Format blank spaces
                this.xml = Regex.Replace(this.xml, "(\\s+)(</i>|</b>|</u>)", "$2$1");
                this.xml = Regex.Replace(this.xml, "(<b>|<i>|<u>)(\\s+)", "$2$1");

                // Remove sequental tags
                this.xml = Regex.Replace(this.xml, @"</i>(\s*)<i>", "$1");
                this.xml = Regex.Replace(this.xml, @"</b>(\s*)<b>", "$1");
                this.xml = Regex.Replace(this.xml, @"<i>(\s*)</i>", "$1");
                this.xml = Regex.Replace(this.xml, @"<b>(\s*)</b>", "$1");
                this.xml = Regex.Replace(this.xml, @"<sup>(\s*)</sup>", "$1");
                this.xml = Regex.Replace(this.xml, @"<sub>(\s*)</sub>", "$1");
                this.xml = Regex.Replace(this.xml, @"(</i>|</b>)(\w)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(</i>)(<b>)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(</b>)(<i>)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(“|‘)(\s+)(<i>)", "$2$1$3");
                this.xml = Regex.Replace(this.xml, @"(\s*’\s*)(</i>)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"(<i>)(\s*‘\s*)", "$2$1");
            }

            private void BoldItalic()
            {
                this.BoldItalicSpaces();
                this.xml = Regex.Replace(this.xml, @"(\s*\(\s*)(</i>|</b>)", "$2 $1");
                this.xml = Regex.Replace(this.xml, @"(<i>|<b>)(\s*\)s*)", "$2 $1");
                this.xml = Regex.Replace(this.xml, @"(</i>)(\()", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(?<!\&[a-z]+)([,;:]+)(</i>)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"(<i>|<b>)([\.,;:]+)", "$2 $1");
                this.xml = Regex.Replace(this.xml, @"([,\.;])(<i>|<b>)", "$1 $2");
                this.xml = Regex.Replace(this.xml, @"(</i>|</b>)\s+([,\.;])", "$1$2 ");
                this.xml = Regex.Replace(this.xml, @"(<b>|<i>)([,\.;\-–])(</b>|</i>)", "$2");
                this.xml = Regex.Replace(this.xml, @"(<i>)([A-Z][a-z]{0,2}|[a-z]{0,3})(</i>)(\.)", "$1$2$4$3");

                // Genus + (Subgenus)
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");

                // Genus + (Subgenus) + species
                this.xml = Regex.Replace(this.xml, @"<i>([A-Z][a-z\.]+\s\([A-Z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");

                // sensu lato & sensu stricto
                this.xml = Regex.Replace(this.xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
                this.xml = Regex.Replace(this.xml, @"<i>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
                this.xml = Regex.Replace(this.xml, @"<i>(s(ensu|\.))\s*(l|s)</i>\.", "<i>$1 $3.</i>");

                // Remove empty tags
                this.xml = Regex.Replace(
                    this.xml,
                    @"(<i></i>|<b></b>|<sup></sup>|<sub></sub>|<u></u>|<i\s*/>|<b\*/>|<sup\s*/>|<sub\s*/>)",
                    string.Empty);

                // Remove bold and italic around punctuation
                this.xml = Regex.Replace(this.xml, @"<(b|i)>([,;\.\-\:\s–]+)</(b|i)>", "$2");
                for (int i = 0; i < 6; i++)
                {
                    // Genus[ species[ subspecies]]
                    this.xml = Regex.Replace(
                        this.xml,
                        @"<i>([A-Z][a-z\.]+([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>",
                        "<i>$1</i>$3<i>$4</i>");

                    // Genus (Subgenus)[ species[ subspecies]]
                    this.xml = Regex.Replace(
                        this.xml,
                        @"<i>([A-Z][a-z\.]+\s*\(\s*[A-Z][a-z\.]+\s*\)([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>",
                        "<i>$1</i>$3<i>$4</i>");
                }

                this.xml = Regex.Replace(this.xml, "</p>\\s*<p", "</p>\n<p");

                this.xml = Regex.Replace(this.xml, @"([A-Z][a-z\.-]+)\s+(s+p+)</i>\.", "$1</i> $2.");
            }

            private void ClearTagsFromTags()
            {
                this.xml = Regex.Replace(this.xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
            }

            private void FormatCloseTags()
            {
                this.xml = Regex.Replace(this.xml, @"(\s+)(</b>|</i>|</u>)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"\s+(</value>|</p>|</th>|</td>)", "$1");
            }

            private void FormatOpenTags()
            {
                this.xml = Regex.Replace(this.xml, @"(<(value|td|th)(\s+[^>]*>|>))\s+", "$1");
                this.xml = Regex.Replace(this.xml, @"(<(b|i|u|sub|sup)(\s+[^>]*>|>))(\s+)", "$4$1");
            }

            private void FormatPunctuation()
            {
                // Format brakets
                this.xml = Regex.Replace(this.xml, @"([\(\[])(\s+)", "$2$1");
                this.xml = Regex.Replace(this.xml, @"(\s+)([\)\]])", "$2$1");
                this.xml = Regex.Replace(this.xml, @"([\)\]])(\s+)([\)\]])", "$1$3$2");
                this.xml = Regex.Replace(this.xml, @"([\(\[])(\s+)([\(\[])", "$2$1$3");

                // Format other punctuation
                this.xml = Regex.Replace(this.xml, @"(\s+)([,;\.])", "$2$1");
                this.xml = Regex.Replace(this.xml, @"([,\.\;])(<i>|<b>)", "$1 $2");
            }

            private void RemoveEmptyTags()
            {
                this.xml = Regex.Replace(this.xml, @"<(i|b|u|sup|sub)(\s+[^>]*>|>)(\s*)</(i|b|u|sup|sub)>", "$3");
                this.xml = Regex.Replace(this.xml, @"<i\s*/>|<b\s*/>|<u\s*/>|<sup\s*/>|<sub\s*/>", string.Empty);
            }
        }
    }
}
