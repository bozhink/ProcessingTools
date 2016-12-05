namespace ProcessingTools.Special.Processors.Processors
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Special.Processors.Contracts;

    public class Flora : IFlora
    {
        private const string InfraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
        private const string LowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

        public void ParseInfra(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var nodeList = document.SelectNodes("//tn");
            foreach (XmlNode node in nodeList)
            {
                node.InnerXml = node.InnerXml
                    .RegexReplace(
                        @"([Vv]ar\.)\s*([a-z\.-]+)",
                        "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"variety\">$2</tn-part>")
                    .RegexReplace(
                        @"([Ff]orma)\s*([a-z\.-]+)",
                        "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"forma\">$2</tn-part>")
                    .RegexReplace(
                        @"([Ss]ub\s*sp\.|[Ss]sp\.|[Ss]pp\.)\s*([a-z\.-]+)",
                        "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>")
                    .RegexReplace(
                        @"([Ss]ect\.|[Ss]ection)\s*([A-Z]?[a-z\.-]+)",
                        "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"section\">$2</tn-part>")
                    .RegexReplace(
                        @"([Ss]ub\s*sect\.|[Ss]ub\s*section)\s*([A-Z]?[a-z\.-]+)",
                        "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subsection\">$2</tn-part>");
            }

            document.Xml = document.Xml.RegexReplace("(?<=</tn-part>)(?=<tn)", " ");
        }

        public void ParseTn(IDocument document, XmlDocument template)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            XmlNode root = template.DocumentElement;

            // Get only full-named taxa
            var templateList = root.SelectNodes("//taxon[count(replace/tn/tn-part[normalize-space(.)=''])=0]");

            document.SelectNodes("//tn")
                .AsParallel()
                .ForAll(node =>
                {
                    for (int i = templateList.Count - 1; i >= 0; i--)
                    {
                        XmlNode taxon = templateList.Item(i);
                        XmlNode find = taxon.FirstChild;
                        XmlNode replace = taxon.LastChild.FirstChild;

                        if (find.InnerText.Length > 2)
                        {
                            string pattern = Regex.Escape(find.InnerText)
                                .RegexReplace(@"([^\w\.])\\ ", "$1?\\s*")
                                .RegexReplace("\\s+", "\\b\\s*\\b");
                            pattern = "(?<!\">)(?<!=\")" + pattern + "(?!\")(?!</tn-part)";

                            if (Regex.Match(node.InnerXml, pattern).Success)
                            {
                                node.InnerXml = Regex.Replace(node.InnerXml, pattern, replace.InnerXml);
                            }
                        }
                    }
                });

            document.Xml = Regex.Replace(document.Xml, "(?<=</tn-part>)(?=<tn)", " ");
        }

        public void PerformReplace(IDocument document, XmlDocument template)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            XmlNode root = template.DocumentElement;

            {
                string xml = document.Xml;

                for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
                {
                    XmlNode taxon = root.ChildNodes.Item(i);
                    XmlNode find = taxon.FirstChild;
                    XmlNode replace = taxon.LastChild;

                    string pattern = Regex.Escape(find.InnerXml)
                        .RegexReplace(@"(\W)\\ ", "$1?\\s*")
                        .RegexReplace("\\s+", "\\b\\s*\\b");

                    xml = xml.RegexReplace(
                        "(?<![a-z-])(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
                        "<tn>$1</tn>");
                }

                xml = xml
                    .RegexReplace(InfraspecificPattern + "\\s*<tn>", "<tn>$1 ")
                    .RegexReplace("(?<!<tn>)(" + InfraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>")
                    .RegexReplace(@"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>")
                    .RegexReplace("(<tn>)" + InfraspecificPattern + "</tn>\\s+<tn>", "$1$2 ")
                    .RegexReplace("</tn>\\s*<tn>" + InfraspecificPattern, " $1");

                document.Xml = xml;
            }

            // Here we must remove tn/tn
            this.RemoveInnerTnNodes(document, "//tn[name(..)!='tn'][count(.//tn)!=0]");

            // Guess new taxa:
            this.GuessNewTaxa(document);

            // Here we must remove tn/tn
            this.RemoveInnerTnNodes(document, "//tn[name(..)!='tn'][count(.//tn)!=0]");

            // Remove taxa in toTaxon
            this.RemoveInnerTnNodes(document, "//toTaxon[count(.//tn)!=0]");
        }

        private void GuessNewTaxa(IDocument document)
        {
            string xml = document.Xml;

            for (int i = 0; i < 10; i++)
            {
                xml = xml
                    .RegexReplace(
                    "(</tn>,?(\\s+and)?\\s+)(" + InfraspecificPattern + "?" + LowerPattern + ")",
                    "$1<tn>$3</tn>");
            }

            // Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
            xml = xml
                .RegexReplace(
                    @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)",
                    "$1<tn>$2</tn>")
                .RegexReplace(
                    "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + InfraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)",
                    "<tn>$1</tn>")
                .RegexReplace(
                    "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + InfraspecificPattern + ")?" + LowerPattern + ")",
                "<tn>$1</tn>");

            document.Xml = xml;
        }

        private void RemoveInnerTnNodes(IDocument document, string xpath)
        {
            var nodeList = document.SelectNodes(xpath);
            foreach (var node in nodeList)
            {
                node.InnerXml = node.InnerXml
                    .RegexReplace(@"</?tn(>| [^>]*>)", string.Empty);
            }
        }
    }
}
