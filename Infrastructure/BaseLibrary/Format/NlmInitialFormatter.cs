namespace ProcessingTools.BaseLibrary.Format
{
    using System.Text.RegularExpressions;
    using System.Xml;

    using Configurator;
    using Contracts;
    using Extensions;

    public class NlmInitialFormatter : Base, IBaseFormatter
    {
        public NlmInitialFormatter(string xml)
            : base(xml)
        {
        }

        public NlmInitialFormatter(Config config, string xml)
            : base(config, xml)
        {
        }

        public NlmInitialFormatter(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Format()
        {
            this.TrimBlockElements();

            this.Xml = this.Xml
                .RegexReplace(@"[^\S\r\n ]+", " ")
                .RegexReplace(@"\&lt;\s*br\s*/\s*\&gt;", "<break />");

            this.XmlDocument.RemoveXmlNodes("//break[count(ancestor::aff) + count(ancestor::alt-title) + count(ancestor::article-title) + count(ancestor::chem-struct) + count(ancestor::disp-formula) + count(ancestor::product) + count(ancestor::sig) + count(ancestor::sig-block) + count(ancestor::subtitle) + count(ancestor::td) + count(ancestor::th) + count(ancestor::title) + count(ancestor::trans-subtitle) + count(ancestor::trans-title) = 0]");

            this.InitialRefactor();

            this.RefactorEmailTags();

            this.FinalRefactor();

            this.TrimBlockElements();
        }

        private void FinalRefactor()
        {
            string xml = this.Xml;

            // Subspecies, subgenera, etc.
            xml = xml
                .RegexReplace(@"<i>([A-Z][a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>")
                .RegexReplace(@"<i>([A-Z][a-z]*\.\s+[a-z]+)</i>\.\s*<i>([a-z]+)</i>", "<i>$1. $2</i>")
                .RegexReplace(@"(<i>[A-Z][a-z]*\.?\s*\([A-Za-z][a-z]*\.?)(</i>)(\))", "$1$3$2")
                .RegexReplace(@"</i>\s+<i>", " ")
                .RegexReplace(@"</i><i>", string.Empty);

            // Supplementary materials external link
            xml = Regex.Replace(xml, "(<ext-link ext-link-type=\")uri(\" [^>]*xlink:href=\")doi:\\s+", "$1doi$2");

            // sensu lato & stricto
            {
                const string SensuSelector = @"(?:(?i)(?:\bs\.\s*|\bsens?u?\.?\s+)[sl][a-z]*\.?)";
                xml = xml
                    .RegexReplace(@"(?<=\W" + SensuSelector + @")</i>\.", ".</i>")
                    .RegexReplace(@"(?<=\w)\s+(?=" + SensuSelector + @"</i>)", "</i> <i>");
            }

            xml = this.RemoveEmptyTags(xml);

            // Remove empty lines
            xml = Regex.Replace(xml, "\n\\s*(?=\n)", string.Empty);

            this.Xml = xml;
        }

        private void InitialRefactor()
        {
            string xml = this.Xml;

            xml = this.FormatOpenCloseTags(xml);

            xml = this.BoldItalic(xml);

            // Format wrong figures' labels
            xml = Regex.Replace(xml, @"(\s*)(<caption>\s*<p>)\s*<b>\s*((Figure|Map|Plate|Table|Suppl|Box)[^<>]*?)\s*</b>", "$1<label>$3</label>$1$2");

            xml = this.FormatReferances(xml);

            // Format DOI notations
            xml = Regex.Replace(xml, @"(?i)(?<=\bdoi:?)[^\S ]*(?=\d)", " ");

            xml = this.FormatPageBreaks(xml);

            // male and female
            xml = Regex.Replace(xml, @"<i>([♂♀\s]+)</i>", "$1");

            // Post-formatting
            for (int i = 0; i < 3; i++)
            {
                xml = this.BoldItalic(xml);
                xml = this.FormatPunctuation(xml);
                xml = this.RemoveEmptyTags(xml);
                xml = this.FormatOpenCloseTags(xml);
            }

            this.Xml = xml;
        }

        private void TrimBlockElements()
        {
            const string XPath = "//title|//label|//article-title|//th|//td|//def|//p[name(..)!='def']|//license-p|//li|//attrib|//kwd|//mixed-citation|//xref-group|//tp:nomenclature-citation";

            foreach (XmlNode node in this.XmlDocument.SelectNodes(XPath, this.NamespaceManager))
            {
                node.InnerXml = node.InnerXml.Replace('\r', ' ').Replace('\n', ' ').Trim();
                node.InnerXml = Regex.Replace(node.InnerXml, @"\s{2,}", " ");
            }
        }

        private string BoldItalicSpaces(string xml)
        {
            string result = xml;

            result = result
                .RegexReplace(@"(<i>|<b>|<u>|<sub>|<sup>)\s+", " $1")
                .RegexReplace(@"\s+(</i>|</b>|</u>|</sub>|</sup>)", "$1 ")
                .RegexReplace(@"([\(\[])\s+(<i>|<b>|<u>|<sub>|<sup>)\s*", " $1$2")
                .RegexReplace(@"\s*(</i>|</b>|</u>|</sub>|</sup>)\s+([\)\]])", "$1$2 ")
                .RegexReplace(@"(</b>\s+<b>|</i>\s+<i>|<b>\s+</b>|<i>\s+</i>)", " ")
                .RegexReplace(@"(</b><b>|<b></b>|</i><i>|<i></i>)", string.Empty);

            return result;
        }

        private string BoldItalic(string xml)
        {
            string result = xml;

            result = this.BoldItalicSpaces(result)
                .RegexReplace(@"(?<!\&[a-z]+)([^\w<>\.\(\)]+)(</i>|</b>)", "$2$1")
                .RegexReplace(@"(<i>|<b>)([^\w<>\.\(\)\&]+)", "$2$1")
                .RegexReplace(@"(<i>)([A-Za-z][a-z]{0,2})(</i>)(\.)", "$1$2$4$3")
                .RegexReplace(@"\s*\(\s*(</i>|</b>)", "$1 (")
                .RegexReplace(@"(<i>|<b>)\s*\)\s*", ") $1")
                .RegexReplace(@"(<b>|<i>)([,\s\.:;\-––])(</b>|</i>)", "$2")
                .RegexReplace(@"(</i>)(\()", "$1 $2")
                .RegexReplace(@"(<i>)([\.,;:\s]+)", "$2$1");

            // Genus + (Subgenus)
            result = Regex.Replace(result, @"<i>([A-Z][a-z\.]+)</i>\s*\(\s*<i>([A-Za-z][a-z\.]+)</i>\s*\)", "<i>$1 ($2)</i>");

            // Genus + (Subgenus) + species
            result = Regex.Replace(result, @"<i>([A-Z][a-z\.]+\s\([A-Za-z][a-z\.]+\))</i>\s*<i>([a-z\.\-]+)</i>", "<i>$1 $2</i>");

            result = result
                .RegexReplace(@"\.</i>(?=</p>)", "</i>.");

            return result;
        }

        private string FormatOpenCloseTags(string xml)
        {
            return xml
                .RegexReplace(
                    @"(<(?:source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)\b[^>]*>)\s+",
                    " $1")
                .RegexReplace(
                    @"\s+(</(?:source|issue-title|b|i|u|monospace|year|month|day|volume|fpage|lpage)>)",
                    "$1 ");
        }

        private string FormatPunctuation(string xml)
        {
            string result = xml;

            result = result
                .RegexReplace(@"\s*([\(\[])\s+", " $1")
                .RegexReplace(@"\s+([\)\]])\s*", "$1 ")
                .RegexReplace(@"([\)\]])\s+([\)\]])", "$1$2 ")
                .RegexReplace(@"([\(\[])\s+([\(\[])", " $1$2");

            return result;
        }

        private string FormatPageBreaks(string xml)
        {
            string result = xml;

            result = result
                .RegexReplace(@"(<!--PageBreak-->)(\s+)(<!--PageBreak-->)", "$1$3$2")
                .RegexReplace(@"(<p>|<tp:nomenclature-citation>|<title>|<label>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"<(t[hd])\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(<t[hd]\b[^>]*>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"<(tr)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(<tr\b[^>]*>)((?:\s*<!--PageBreak-->\s*)+)", "$2$1")
                .RegexReplace(@"((\s*)<ref [^>]*>.*?)((<!--PageBreak-->)+)", "$2$3$1")
                .RegexReplace(@"(<i>[^<>]*?)((<!--PageBreak-->)+)", "$2$1")
                .RegexReplace(@"(<kwd>.*?)((?:\s*<!--PageBreak-->)+)(.*</kwd>)", "$2$1$3")
                .RegexReplace(@"<(title|label|kwd|p)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"<(b|i)\b[^>]*>((?:\s*<!--PageBreak-->\s*)+)</\1>", "$2")
                .RegexReplace(@"(\s*)(<xref-group>[\s\S]*?)((<!--PageBreak-->)+)([\s\S]*?</xref-group>)", "$1$2$5$1$3");

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

            result = result
                .RegexReplace(@"(\S)(<article-title>|<source>)", "$1 $2")
                .RegexReplace(@"(</article-title>|</source>)(\S)", "$1 $2")
                .RegexReplace(@"(</source>)((?i)doi:?)", "$1 $2")
                .RegexReplace(@"(?<=</surname>)\s*(?=<given-names>)", " ")
                .RegexReplace(@"</volume>\s+\(<issue>", "</volume>(<issue>")
                .RegexReplace(@"</issue>(\))+", "</issue>)")
                .RegexReplace(@"<role>Ed</role>", "<role>Ed.</role>")
                .RegexReplace(@"(?<=</role>\))[\.,;:]", string.Empty);

            return result;
        }

        private void RefactorEmailTags()
        {
            Regex matchMultipleEmails = new Regex(@"(?<!<[^<>]+)(?<=\w)(\W*\s+\W*)(?=\w)(?![^<>]+>)");
            foreach (XmlNode email in this.XmlDocument.SelectNodes("//email", this.NamespaceManager))
            {
                email.InnerXml = email.InnerXml
                    .RegexReplace(@"\A\s+|\s+\Z", string.Empty);

                if (matchMultipleEmails.IsMatch(email.InnerXml))
                {
                    XmlDocumentFragment fragment = this.XmlDocument.CreateDocumentFragment();
                    fragment.InnerXml = matchMultipleEmails.Replace(email.OuterXml, @"</email>$1<email>");
                    email.ParentNode.ReplaceChild(fragment, email);
                }
            }
        }
    }
}