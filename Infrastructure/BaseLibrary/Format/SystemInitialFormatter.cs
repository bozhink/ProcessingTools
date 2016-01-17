namespace ProcessingTools.BaseLibrary.Format
{
    using System.Text.RegularExpressions;

    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts;

    public class SystemInitialFormatter : Base, IFormatter
    {
        public SystemInitialFormatter(string xml)
            : base(xml)
        {
        }

        public SystemInitialFormatter(Config config, string xml)
            : base(config, xml)
        {
        }

        public void Format()
        {
            this.FormatCloseTags();
            this.FormatOpenTags();
            this.BoldItalic();

            {
                string xml = this.Xml;

                Regex matchParagraphs = new Regex("<p>[\\S\\s]*?</p>");
                for (Match paragraph = matchParagraphs.Match(xml); paragraph.Success; paragraph = paragraph.NextMatch())
                {
                    string replace = Regex.Replace(paragraph.Value, "\n", " ");
                    xml = Regex.Replace(xml, Regex.Escape(paragraph.Value), replace);
                }

                this.Xml = xml;
            }

            // male and female
            this.Xml = Regex.Replace(this.Xml, "<i>([♂♀\\s]+)</i>", "$1");

            // Post-formatting
            for (int i = 0; i < 3; i++)
            {
                this.BoldItalic();
                this.FormatPunctuation();
                this.RemoveEmptyTags();
                this.FormatCloseTags();
                this.FormatOpenTags();
            }

            {
                string xml = this.Xml;

                xml = Regex.Replace(xml, @"<\s+/", "</");

                // Remove empty lines
                xml = Regex.Replace(xml, @"\n\s*\n", "\n");

                // sensu lato & stricto
                xml = Regex.Replace(xml, @"(<i>)((s\.|sens?u?)\s+[sl][a-z]*)(</i>)\.", "$1$2.$4");

                xml = Regex.Replace(xml, @"<\s+/", "</");

                this.Xml = xml;
            }
        }

        private void BoldItalicSpaces()
        {
            string xml = this.Xml;

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

            this.Xml = xml;
        }

        private void BoldItalic()
        {
            this.BoldItalicSpaces();

            string xml = this.Xml;

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
            xml = Regex.Replace(
                xml,
                @"(<i></i>|<b></b>|<sup></sup>|<sub></sub>|<u></u>|<i\s*/>|<b\*/>|<sup\s*/>|<sub\s*/>)",
                string.Empty);

            // Remove bold and italic around punctuation
            xml = Regex.Replace(xml, @"<(b|i)>([,;\.\-\:\s–]+)</(b|i)>", "$2");
            for (int i = 0; i < 6; i++)
            {
                // Genus[ species[ subspecies]]
                xml = Regex.Replace(
                    xml,
                    @"<i>([A-Z][a-z\.]+([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>",
                    "<i>$1</i>$3<i>$4</i>");

                // Genus (Subgenus)[ species[ subspecies]]
                xml = Regex.Replace(
                    xml,
                    @"<i>([A-Z][a-z\.]+\s*\(\s*[A-Z][a-z\.]+\s*\)([a-z\.\s]*[a-z])?)\s*([,;\.]\s*)([^<>]*)</i>",
                    "<i>$1</i>$3<i>$4</i>");
            }

            xml = Regex.Replace(xml, "</p>\\s*<p", "</p>\n<p");
            xml = Regex.Replace(xml, @"([A-Z][a-z\.-]+)\s+(s+p+)</i>\.", "$1</i> $2.");

            this.Xml = xml;
        }

        private void ClearTagsFromTags()
        {
            this.Xml = Regex.Replace(this.Xml, "(<[^<>]*)(<[^>]*>)([^<>]*)(</[^>]*>)([^>]*>)", "$1$3$5");
        }

        private void FormatCloseTags()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, @"(\s+)(</b>|</i>|</u>)", "$2$1");
            xml = Regex.Replace(xml, @"\s+(</value>|</p>|</th>|</td>)", "$1");

            this.Xml = xml;
        }

        private void FormatOpenTags()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, @"(<(value|td|th)(\s+[^>]*>|>))\s+", "$1");
            xml = Regex.Replace(xml, @"(<(b|i|u|sub|sup)(\s+[^>]*>|>))(\s+)", "$4$1");

            this.Xml = xml;
        }

        private void FormatPunctuation()
        {
            string xml = this.Xml;

            // Format brakets
            xml = Regex.Replace(xml, @"([\(\[])(\s+)", "$2$1");
            xml = Regex.Replace(xml, @"(\s+)([\)\]])", "$2$1");
            xml = Regex.Replace(xml, @"([\)\]])(\s+)([\)\]])", "$1$3$2");
            xml = Regex.Replace(xml, @"([\(\[])(\s+)([\(\[])", "$2$1$3");

            // Format other punctuation
            xml = Regex.Replace(xml, @"(\s+)([,;])", "$2$1");
            xml = Regex.Replace(xml, @"([,\.\;])(<i>|<b>)", "$1 $2");

            this.Xml = xml;
        }

        private void RemoveEmptyTags()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, @"<(i|b|u|sup|sub)(\s+[^>]*>|>)(\s*)</(i|b|u|sup|sub)>", "$3");
            xml = Regex.Replace(xml, @"<i\s*/>|<b\s*/>|<u\s*/>|<sup\s*/>|<sub\s*/>", string.Empty);

            this.Xml = xml;
        }
    }
}