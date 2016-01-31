namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Configurator;
    using Extensions;

    public class Flora : Base
    {
        public Flora(Config config, string xml)
            : base(config, xml)
        {
        }

        public static string DistinctTaxa(string xml)
        {
            return xml.ApplyXslTransform(@"C:\bin\taxa.distinct.xslt");
        }

        public void ExtractTaxa()
        {
            this.Xml = this.XmlDocument.ApplyXslTransform(this.Config.FloraExtractTaxaXslPath);
        }

        public string ExtractTaxaParts()
        {
            return this.XmlDocument.ApplyXslTransform(this.Config.FloraExtractTaxaPartsXslPath);
        }

        public void DistinctTaxa()
        {
            this.Xml = this.XmlDocument.ApplyXslTransform(this.Config.FloraDistrinctTaxaXslPath);
        }

        public void GenerateTagTemplate()
        {
            XmlDocument generatedTemplate = new XmlDocument();
            generatedTemplate.LoadXml(Flora.DistinctTaxa(this.XmlDocument.ApplyXslTransform(this.Config.FloraGenerateTemplatesXslPath)));
            generatedTemplate.Save(this.Config.FloraTemplatesOutputXmlPath);
        }

        public void PerformReplace()
        {
            const string InfraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
            const string LowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

            XmlDocument template = new XmlDocument();
            template.Load(this.Config.FloraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;

            {
                string xml = this.XmlDocument.OuterXml;

                for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
                {
                    XmlNode taxon = root.ChildNodes.Item(i);
                    XmlNode find = taxon.FirstChild;
                    XmlNode replace = taxon.LastChild;

                    string pattern = Regex.Replace(Regex.Escape(find.InnerXml), @"(\W)\\ ", "$1?\\s*");
                    pattern = Regex.Replace(pattern, "\\s+", "\\b\\s*\\b");

                    xml = Regex.Replace(
                        xml,
                        "(?<![a-z-])(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
                        "<tn>$1</tn>");
                }

                xml = Regex.Replace(xml, InfraspecificPattern + "\\s*<tn>", "<tn>$1 ");
                xml = Regex.Replace(xml, "(?<!<tn>)(" + InfraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

                xml = Regex.Replace(xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
                xml = Regex.Replace(xml, "(<tn>)" + InfraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

                xml = Regex.Replace(xml, "</tn>\\s*<tn>" + InfraspecificPattern, " $1");

                this.Xml = xml;
            }

            // Here we must remove tn/tn
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }
            }

            // Guess new taxa:
            {
                string xml = this.Xml;

                for (int i = 0; i < 10; i++)
                {
                    xml = Regex.Replace(
                        xml,
                        "(</tn>,?(\\s+and)?\\s+)(" + InfraspecificPattern + "?" + LowerPattern + ")",
                        "$1<tn>$3</tn>");
                }

                // Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
                xml = Regex.Replace(xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

                xml = Regex.Replace(xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + InfraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

                xml = Regex.Replace(
                    xml,
                    "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + InfraspecificPattern + ")?" + LowerPattern + ")",
                    "<tn>$1</tn>");

                this.Xml = xml;
            }

            // Here we must remove tn/tn
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }
            }

            // Remove taxa in toTaxon
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }
            }
        }

        public void ParseInfra()
        {
            XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn");
            foreach (XmlNode node in nodeList)
            {
                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"([Vv]ar\.)\s*([a-z\.-]+)",
                    "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"variety\">$2</tn-part>");

                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"([Ff]orma)\s*([a-z\.-]+)",
                    "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"forma\">$2</tn-part>");

                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"([Ss]ub\s*sp\.|[Ss]sp\.|[Ss]pp\.)\s*([a-z\.-]+)",
                    "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subspecies\">$2</tn-part>");

                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"([Ss]ect\.|[Ss]ection)\s*([A-Z]?[a-z\.-]+)",
                    "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"section\">$2</tn-part>");

                node.InnerXml = Regex.Replace(
                    node.InnerXml,
                    @"([Ss]ub\s*sect\.|[Ss]ub\s*section)\s*([A-Z]?[a-z\.-]+)",
                    "<tn-part type=\"infrank\">$1</tn-part> <tn-part type=\"subsection\">$2</tn-part>");
            }

            this.Xml = Regex.Replace(this.Xml, "(?<=</tn-part>)(?=<tn)", " ");
        }

        public void ParseTn()
        {
            XmlDocument template = new XmlDocument();
            template.Load(this.Config.FloraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;

            // Get only full-named taxa
            XmlNodeList templateList = root.SelectNodes("//taxon[count(replace/tn/tn-part[normalize-space(.)=''])=0]");

            XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn");

            Parallel.For(0, nodeList.Count, index => ParseTnParallelCallBackFunction(index, templateList, nodeList));

            this.Xml = Regex.Replace(this.Xml, "(?<=</tn-part>)(?=<tn)", " ");
        }

        private static void ParseTnParallelCallBackFunction(int index, XmlNodeList templateList, XmlNodeList nodeList)
        {
            XmlNode node = nodeList.Item(index);
            for (int i = templateList.Count - 1; i >= 0; i--)
            {
                XmlNode taxon = templateList.Item(i);
                XmlNode find = taxon.FirstChild;
                XmlNode replace = taxon.LastChild.FirstChild;

                if (find.InnerText.Length > 2)
                {
                    string pattern = Regex.Replace(Regex.Escape(find.InnerText), @"([^\w\.])\\ ", "$1?\\s*");
                    pattern = Regex.Replace(pattern, "\\s+", "\\b\\s*\\b");
                    pattern = "(?<!\">)(?<!=\")" + pattern + "(?!\")(?!</tn-part)";

                    if (Regex.Match(node.InnerXml, pattern).Success)
                    {
                        node.InnerXml = Regex.Replace(node.InnerXml, pattern, replace.InnerXml);
                    }
                }
            }
        }
    }
}
