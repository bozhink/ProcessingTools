using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Base
{
    public class Flora : TaggerBase
    {
        public Flora(string xml)
            : base(xml)
        {
        }

        public Flora(Config config, string xml)
            : base(config, xml)
        {
        }

        public Flora(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public static string DistinctTaxa(string xml)
        {
            return XsltOnString.ApplyTransform(@"C:\bin\taxa.distinct.xslt", xml);
        }

        public void ExtractTaxa()
        {
            this.Xml = XsltOnString.ApplyTransform(this.Config.floraExtractTaxaXslPath, this.Xml);
        }

        public string ExtractTaxaParts()
        {
            return XsltOnString.ApplyTransform(this.Config.floraExtractTaxaPartsXslPath, this.Xml);
        }

        public void DistinctTaxa()
        {
            this.Xml = XsltOnString.ApplyTransform(this.Config.floraDistrinctTaxaXslPath, this.Xml);
        }

        public void GenerateTagTemplate()
        {
            XmlDocument generatedTemplate = new XmlDocument();
            generatedTemplate.LoadXml(Flora.DistinctTaxa(XsltOnString.ApplyTransform(this.Config.floraGenerateTemplatesXslPath, this.Xml)));
            generatedTemplate.Save(this.Config.floraTemplatesOutputXmlPath);
        }

        public void PerformReplace()
        {
            const string InfraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
            const string LowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

            this.ParseXmlStringToXmlDocument();

            XmlDocument template = new XmlDocument();
            template.Load(this.Config.floraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;
            Alert.Log(root.ChildNodes.Count);

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

            // TODO: Here we must remove tn/tn
            {
                this.ParseXmlStringToXmlDocument();

                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.ParseXmlDocumentToXmlString();
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

            // TODO: Here we must remove tn/tn
            {
                this.ParseXmlStringToXmlDocument();

                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.ParseXmlDocumentToXmlString();
            }

            // Remove taxa in toTaxon
            {
                this.ParseXmlStringToXmlDocument();

                XmlNodeList nodeList = this.XmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.ParseXmlDocumentToXmlString();
            }
        }

        public void ParseInfra()
        {
            this.ParseXmlStringToXmlDocument();

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

            this.ParseXmlDocumentToXmlString();
            this.Xml = Regex.Replace(this.Xml, "(?<=</tn-part>)(?=<tn)", " ");
        }

        public void ParseTn()
        {
            this.ParseXmlStringToXmlDocument();

            XmlDocument template = new XmlDocument();
            template.Load(this.Config.floraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;

            // Get only full-named taxa
            XmlNodeList templateList = root.SelectNodes("//taxon[count(replace/tn/tn-part[normalize-space(.)=''])=0]");
            Alert.Log(templateList.Count);

            XmlNodeList nodeList = this.XmlDocument.SelectNodes("//tn");
            Alert.Log(nodeList.Count);

            Parallel.For(0, nodeList.Count, index => ParseTnParallelCallBackFunction(index, templateList, nodeList));

            this.ParseXmlDocumentToXmlString();
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
