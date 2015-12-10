namespace ProcessingTools.BaseLibrary.Format.Nlm
{
    using System.Text.RegularExpressions;
    using System.Xml;

    using Configurator;
    using Contracts;

    public class Formatter : Base, IBaseFormatter
    {
        public Formatter(string xml)
            : base(xml)
        {
        }

        public Formatter(Config config, string xml)
            : base(config, xml)
        {
        }

        public Formatter(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Format()
        {
            string xml = this.Xml;

            // Remove white spaces in @id and @rid values
            xml = Regex.Replace(xml, @"(?<=<[^>]+\br?id="")\s*([^<>""]*?)\s*(?="")", "$1");

            // Format openeing and closing tags
            xml = this.FormatCloseTags(xml);
            xml = this.FormatOpenTags(xml);
            xml = this.BoldItalic(xml);

            // Format wrong figures' labels
            xml = Regex.Replace(xml, @"<caption>\s+<p>", "<caption><p>");
            xml = Regex.Replace(xml, @"</p>\s+</caption>", "</p></caption>");
            xml = Regex.Replace(xml, @"(\s*)(<caption><p>)\s*<b>\s*((Figure|Map|Plate|Table|Suppl|Box)[^<>]*?)\s*</b>", "$1<label>$3</label>$1$2");

            xml = this.FormatReferances(xml);
            xml = this.FormatPageBreaks(xml);

            // male and female
            xml = Regex.Replace(xml, @"<i>([♂♀\s]+)</i>", "$1");

            // Post-formatting
            for (int i = 0; i < 3; i++)
            {
                xml = this.BoldItalic(xml);
                xml = this.FormatPunctuation(xml);
                ////xml = FormatPageBreaks(xml);
                xml = this.RemoveEmptyTags(xml);
                xml = this.FormatCloseTags(xml);
                xml = this.FormatOpenTags(xml);
            }

            {
                this.Xml = xml;
                foreach (XmlNode node in this.XmlDocument.SelectNodes("//title|//label|//article-title|//th|//td|//p|//license-p|//li|//attrib|//kwd|//ref|//mixed-citation|//object-id|//xref-group|//tp:nomenclature-citation|//self-uri|//name|//given-names|//surname|//person-group|//graphic[string()!='']", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, @"\A\s+|\s+\Z", string.Empty);
                    node.InnerXml = Regex.Replace(node.InnerXml, @"\s+", " ");
                }

                foreach (XmlNode node in this.XmlDocument.SelectNodes("//td|//th", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "&lt;br/&gt;", "<break />");
                }

                foreach (XmlNode node in this.XmlDocument.SelectNodes("//front//contrib", this.NamespaceManager))
                {
                    node.InnerXml = Regex.Replace(
                        node.InnerXml,
                        @"(\s*)(<name [^>]*>)\s*(<surname>.*?</surname>)\s*(<given-names>.*?</given-names>)\s*(</name>)",
                        "$1$2$1    $3$1    $4$1$5");
                }

                xml = this.Xml;
            }

            // Subspecies, subgenera, etc.
            xml = Regex.Replace(xml, @"<i>([A-Z][a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
            xml = Regex.Replace(xml, @"<i>([A-Z][a-z]*\.\s+[a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>");
            xml = Regex.Replace(xml, @"(<i>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</i>)(\))", "$1$3$2");
            xml = Regex.Replace(xml, @"</i>\s+<i>", " ");
            xml = Regex.Replace(xml, @"</i><i>", string.Empty);

            // Clear empty symbols out of <article> tag
            xml = Regex.Replace(xml, @"(?<=</article>)\s+", string.Empty);

            // Supplementary materials external link
            xml = Regex.Replace(xml, "(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2");

            // Misc
            xml = Regex.Replace(xml, @"\s*(<\w[^>]*>\s*)?&lt;br\s*/&gt;(\s*</\w[^>]*>)?\s*(?=</p>\s*</caption>)", string.Empty);
            xml = Regex.Replace(xml, @"\.</i>(?=</p>)", "</i>.");

            // sensu lato & stricto
            {
                const string SensuSelector = @"(?:(?i)(?:\bs\.\s*|\bsens?u?\.?\s+)[sl][a-z]*\.?)";
                xml = Regex.Replace(xml, @"(?<=\W" + SensuSelector + @")</i>\.", ".</i>");
                xml = Regex.Replace(xml, @"(?<=\w)\s+(?=" + SensuSelector + @"</i>)", "</i> <i>");
            }

            xml = Regex.Replace(xml, @"<\s+/", "</");

            xml = this.RemoveEmptyTags(xml);
            xml = this.FormatPageBreaks(xml);

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
            xml = Regex.Replace(xml, "\n\\s*(?=\n)", string.Empty);

            this.Xml = xml;
        }

        private string BoldItalicSpaces(string xml)
        {
            string result = xml;

            result = Regex.Replace(result, @"(<b>|<i>)(\s+)", " $1");
            result = Regex.Replace(result, @"\s+(</b>|</i>)", "$1 ");
            result = Regex.Replace(result, @"(</b>\s+<b>|</i>\s+<i>|<b>\s+</b>|<i>\s+</i>)", " ");
            result = Regex.Replace(result, @"(</b><b>|<b></b>|</i><i>|<i></i>)", string.Empty);
            result = Regex.Replace(result, @"(</italic>|</bold>)(\w)", "$1 $2");
            ////result = Regex.Replace(result, @"(“|‘)\s+(<i>|<b>)", " $1$2");
            ////result = Regex.Replace(result, @"(</i>|</b>)\s+(’)", "$1$2 ");

            result = Regex.Replace(result, @"([\(\[])\s+(<i>|<b>|<u>|<sub>|<sup>)\s*", " $1$2");
            ////result = Regex.Replace(result, @"\s+(</i>|</b>|</u>|</sub>|</sup>)([^,;\)\]\.])", "$1 $2");

            ////result = Regex.Replace(result, @"(</b>|</i>)\s*(<b>|<i>)", "$1 $2");

            result = Regex.Replace(result, @"([,\.;])(<i>|<b>)", "$1 $2");
            result = Regex.Replace(result, @"(</i>|</b>)\s+([,\.;])", "$1$2 ");

            return result;
        }

        private string BoldItalic(string xml)
        {
            string result = xml;

            result = this.BoldItalicSpaces(result);

            result = Regex.Replace(result, @"\&lt;\s*br\s*/\s*\&gt;(</i>|</b>)", "$1&lt;br/&gt;");
            result = Regex.Replace(result, @"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</i>|</b>)", "$2$1");
            result = Regex.Replace(result, @"(<i>|<b>)([^\w<>\.\(\)\&]+)", "$2$1");
            result = Regex.Replace(result, @"(<i>)([A-Za-z][a-z]{0,2})(</i>)(\.)", "$1$2$4$3");
            ////result = Regex.Replace(result, @"(</bold>)([\.:])", "$2$1");

            result = Regex.Replace(result, @"\s*\(\s*(</i>|</b>)", "$1 (");
            result = Regex.Replace(result, @"(<i>|<b>)\s*\)\s*", ") $1");

            ////result = Regex.Replace(result, "(’)(</italic>)", "$2$1");
            ////result = Regex.Replace(result, "(<italic>)(‘)", "$2$1");

            result = Regex.Replace(result, @"(<b>|<i>)([,\s\.:;\-––])(</b>|</i>)", "$2");

            // Genus + (Subgenus)
            result = Regex.Replace(result, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Za-z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");

            // Genus + (Subgenus) + species
            result = Regex.Replace(result, @"<i>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");

            // sensu lato & sensu stricto
            result = Regex.Replace(result, @"<i>([A-Za-z\.\(\)\s\-]+)\s*(sensu\s*.*?)</i>", "<i>$1</i> <i>$2</i>");
            result = Regex.Replace(result, @"<i>([A-Za-z\.\(\)\s\-]+)\s+(s\.\s*[a-z\.]*)</i>", "<i>$1</i> <i>$2</i>");
            result = Regex.Replace(result, @"<i>(s(ensu|\.))\s*(l|s|str)</i>\.", "<i>$1 $3.</i>");

            // Paste some intervals
            ////result = Regex.Replace(result, "</i><b>", "</i> <b>");

            result = Regex.Replace(result, @"(</i>)(\()", "$1 $2");
            result = Regex.Replace(result, @"(<i>)([\.,;:\s]+)", "$2$1");
            result = Regex.Replace(result, @"([‘“])(<i>)", "$2$1");
            result = Regex.Replace(result, @"(</i>)([’”])", "$2$1");

            return result;
        }

        private string FormatCloseTags(string xml)
        {
            return Regex.Replace(xml, @"\s+(</(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)>)", "$1 ");
        }

        private string FormatOpenTags(string xml)
        {
            return Regex.Replace(xml, @"(<(source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)([^>]*)>)\s+", " $1");
        }

        private string FormatPunctuation(string xml)
        {
            string result = xml;

            // Format brakets
            result = Regex.Replace(result, @"(\s*)(\()(\s+)", "$1$3$2");
            result = Regex.Replace(result, @"(\s+)(\))(\s*)", "$3$1$2");
            result = Regex.Replace(result, @"(\)|\])\s+(\)|\])", "$1$2");
            result = Regex.Replace(result, @"(\(|\[)\s+(\(|\[)", "$1$2");

            // Format other punctuation
            result = Regex.Replace(result, @"(\s+)([,;\.])", "$2$1");
            result = Regex.Replace(result, @"([,\.\;])(<i>|<b>)", "$1 $2");

            return result;
        }

        private string FormatPageBreaks(string xml)
        {
            string result = xml;

            result = Regex.Replace(result, @"(<!--PageBreak-->)(\s+)(<!--PageBreak-->)", "$1$3$2");
            result = Regex.Replace(result, @"(\s*)(<p>|<tp:nomenclature-citation>|<title>|<label>)\s*((<!--PageBreak-->)+)\s*", "$1$3$1$2");

            result = Regex.Replace(result, @"<tr[^>]*>\s*<(td|th)[^>]*>\s*((<!--PageBreak-->)+)\s*</(td|th)>\s*</tr>", "$2");
            result = Regex.Replace(result, @"<tr>\s*((<!--PageBreak-->)+)\s*</tr>", "$1");
            result = Regex.Replace(result, @"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1");
            result = Regex.Replace(result, @"(<i>[^<>]*?)((<!--PageBreak-->)+)", "$2$1");

            result = Regex.Replace(result, @"(\s*)(<kwd>.*?)\s*((<!--PageBreak-->)+)(.*</kwd>)", "$1$2$5$1$3");
            result = Regex.Replace(result, @"<(title|label|kwd|p)>\s*((<!--PageBreak-->)+)</(title|label|kwd|p)>", "$2");

            result = Regex.Replace(result, @"(<b>|<i>)((<!--PageBreak-->)+)(</b>|</i>)", "$2");
            result = Regex.Replace(result, @"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");

            result = Regex.Replace(result, @"(<!--PageBreak-->)\s+(<!--PageBreak-->)", "$1$2");

            return result;
        }

        private string RemoveEmptyTags(string xml)
        {
            string result = xml;

            result = Regex.Replace(
                result,
                @"<p>\s*</p>|<xref-group>\s*</xref-group>|<i></i>|<i\s*/>|<sup\s*/>|<sub\s*/>|<b\s*/>|<label\s*/>|<b></b>|<kwd>\s*</kwd>|<sup></sup>|<sub></sub>|<mixed-citation [^>]*>\s*</mixed-citation>|<tp:nomenclature-citation>\s*</tp:nomenclature-citation>|<ref [^>]*>\s*</ref>|<source></source>|<u></u>|<u\s*/>|<monospace></monospace>|<monospace\s*/>",
                string.Empty);

            result = Regex.Replace(result, @"<i>\s+</i>|<b>\s+</b>|<sup>\s+</sup>|<sub>\s+</sub>|<source>\s+</source>|<u>\s+</u>|<monospace>\s+</monospace>", " ");

            return result;
        }

        private string FormatReferances(string xml)
        {
            string result = xml;

            result = Regex.Replace(result, @"(\S)(<article-title>|<source>)", "$1 $2");
            result = Regex.Replace(result, @"(</article-title>|</source>)(\S)", "$1 $2");
            result = Regex.Replace(result, @"(?<=</surname>)\s*(?=<given-names>)", " ");

            result = Regex.Replace(result, @"</volume>\s+\(<issue>", "</volume>(<issue>");
            result = Regex.Replace(result, @"</issue>(\))+", "</issue>)");

            ////result = Regex.Replace(result, @"<role>Ed\.</role>", "<role>Ed</role>");
            result = Regex.Replace(result, @"</role>\)(\.|,|;|:)", "</role>)");

            return result;
        }
    }
}