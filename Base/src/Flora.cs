using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Base
{
    public class Flora : Base
    {
        public Flora()
            : base()
        {
        }

        public Flora(string xml)
            : base(xml)
        {
        }

        public static string DistinctTaxa(string xml)
        {
            return XsltOnString.ApplyTransform(@"C:\bin\taxa.distinct.xslt", xml);
        }

        public void ExtractTaxa()
        {
            this.xml = XsltOnString.ApplyTransform(this.config.floraExtractTaxaXslPath, this.xml);
        }

        public string ExtractTaxaParts()
        {
            return XsltOnString.ApplyTransform(config.floraExtractTaxaPartsXslPath, this.xml);
        }

        public void DistinctTaxa()
        {
            this.xml = XsltOnString.ApplyTransform(config.floraDistrinctTaxaXslPath, this.xml);
        }

        public void GenerateTagTemplate()
        {
            XmlDocument generatedTemplate = new XmlDocument();
            generatedTemplate.LoadXml(Flora.DistinctTaxa(XsltOnString.ApplyTransform(this.config.floraGenerateTemplatesXslPath, this.xml)));
            generatedTemplate.Save(this.config.floraTemplatesOutputXmlPath);
        }

        public void PerformReplace()
        {
            this.ParseXmlStringToXmlDocument();

            XmlDocument template = new XmlDocument();
            template.Load(this.config.floraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;
            Alert.Message(root.ChildNodes.Count);

            this.xml = this.xmlDocument.OuterXml;
            for (int i = root.ChildNodes.Count - 1; i >= 0; i--)
            {
                XmlNode taxon = root.ChildNodes.Item(i);
                XmlNode find = taxon.FirstChild;
                XmlNode replace = taxon.LastChild;

                string pattern = Regex.Replace(Regex.Escape(find.InnerXml), @"(\W)\\ ", "$1?\\s*");
                pattern = Regex.Replace(pattern, "\\s+", "\\b\\s*\\b");

                this.xml = Regex.Replace(
                    this.xml,
                    "(?<![a-z-])(?<!<tn>)(" + pattern + ")(?![A-Za-z])(?!</tn\\W)(?!</tp:)(?!</name>)",
                    "<tn>$1</tn>");
            }

            string infraspecificPattern = "\\b([Vv]ar\\.|[Ss]ubsp\\.|([Ss]ub)?[Ss]ect\\.|[Aa]ff\\.|[Cc]f\\.|[Ff]orma)";
            string lowerPattern = "\\s*\\b[a-z]*(ensis|ulei|onis|oidis|oide?a|phyll[au][sm]?|[aeiou]lii|longiflora)\\b";

            this.xml = Regex.Replace(this.xml, infraspecificPattern + "\\s*<tn>", "<tn>$1 ");
            this.xml = Regex.Replace(this.xml, "(?<!<tn>)(" + infraspecificPattern + "\\s+[A-Z]?[a-z\\.-]+)(?!</tn>)", "<tn>$1</tn>");

            this.xml = Regex.Replace(this.xml, @"<tn>([A-Z][a-z\.-]+)</tn>\s+<tn>([a-z\.-]+)</tn>", "<tn>$1 $2</tn>");
            this.xml = Regex.Replace(this.xml, "(<tn>)" + infraspecificPattern + "</tn>\\s+<tn>", "$1$2 ");

            this.xml = Regex.Replace(this.xml, "</tn>\\s*<tn>" + infraspecificPattern, " $1");

            // TODO: Here we must remove tn/tn
            {
                this.ParseXmlStringToXmlDocument();
                XmlNodeList nodeList = xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.xml = xmlDocument.OuterXml;
            }

            // Guess new taxa:
            for (int i = 0; i < 10; i++)
            {
                this.xml = Regex.Replace(
                    this.xml,
                    "(</tn>,?(\\s+and)?\\s+)(" + infraspecificPattern + "?" + lowerPattern + ")",
                    "$1<tn>$3</tn>");
            }

            // Genus <tn>species</tn>. The result will be <tn>Genus <tn>species</tn></tn>
            this.xml = Regex.Replace(this.xml, @"([^\.\s]\s+)([A-Z][a-z\.-]+\s+<tn>[a-z\.-]+.*?</tn>)", "$1<tn>$2</tn>");

            this.xml = Regex.Replace(this.xml, "\\b([A-Z][a-z\\.-]+(\\s*[a-z\\.-]+)?\\s+<tn>" + infraspecificPattern + "\\s*[a-z\\.-]+.*?</tn>)", "<tn>$1</tn>");

            this.xml = Regex.Replace(
                this.xml,
                "(([A-Z][a-z\\.-]+|<tn>.*?</tn>)\\s+([a-z\\.-]*\\s*" + infraspecificPattern + ")?" + lowerPattern + ")",
                "<tn>$1</tn>");

            // TODO: Here we must remove tn/tn
            {
                this.ParseXmlStringToXmlDocument();
                XmlNodeList nodeList = this.xmlDocument.SelectNodes("//tn[name(..)!='tn'][count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.xml = this.xmlDocument.OuterXml;
            }

            // Remove taxa in toTaxon
            {
                this.ParseXmlStringToXmlDocument();
                XmlNodeList nodeList = this.xmlDocument.SelectNodes("//toTaxon[count(.//tn)!=0]");
                foreach (XmlNode node in nodeList)
                {
                    node.InnerXml = Regex.Replace(node.InnerXml, "</?tn>", string.Empty);
                }

                this.xml = this.xmlDocument.OuterXml;
            }
        }

        public void ParseInfra()
        {
            this.ParseXmlStringToXmlDocument();
            XmlNodeList nodeList = this.xmlDocument.SelectNodes("//tn");
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

            this.xml = this.xmlDocument.OuterXml;
            this.xml = Regex.Replace(this.xml, "(?<=</tn-part>)(?=<tn)", " ");
        }

        public void ParseTn()
        {
            this.ParseXmlStringToXmlDocument();
            XmlDocument template = new XmlDocument();
            template.Load(this.config.floraTemplatesOutputXmlPath);

            XmlNode root = template.DocumentElement;

            // Get only full-named taxa
            XmlNodeList templateList = root.SelectNodes("//taxon[count(replace/tn/tn-part[normalize-space(.)=''])=0]");
            Alert.Message(templateList.Count);

            XmlNodeList nodeList = xmlDocument.SelectNodes("//tn");
            Alert.Message(nodeList.Count);

            Parallel.For(
                0,
                nodeList.Count,
                index =>
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
                });

            this.xml = this.xmlDocument.OuterXml;
            this.xml = Regex.Replace(this.xml, "(?<=</tn-part>)(?=<tn)", " ");
        }
    }
}
