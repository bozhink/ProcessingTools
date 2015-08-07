using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class QuantitiesTagger : TaggerBase
    {
        public QuantitiesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public QuantitiesTagger(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public void TagQuantities(IXPathProvider xpathProvider)
        {
            this.ParseXmlStringToXmlDocument();
            string xpath = string.Format(xpathProvider.SelectContentNodesXPathTemplate, "normalize-space(.)!=''");
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);

            List<string> quantities = new List<string>();

            // 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
            {
                string pattern = @"(?<!<[^>]+)((?:(?:[\(\)\[\]\{\}–—−‒-]\s*)??\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?(?:[kdcmµ]m|meters?|[kmµnp]g|[º°˚]\s*[FC]|[º°˚]|[M]?bp|ft|m|[kdcmµ]M|[dcmµ][lL]|[kdcmµ]mol|mile|mi|min(?:ute)|\%)\b)(?![^<>]*>)";
                Regex matchQuantities = new Regex(pattern);
                quantities = nodeList.GetMatchesInXmlText(matchQuantities, true);
            }

            TagContent quantityTag = new TagContent("quantity");
            foreach (string quantity in quantities)
            {
                Alert.Log(quantity);
                TagTextInXmlDocument(quantity, quantityTag, xpathProvider.SelectContentNodesXPathTemplate, true);
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagDirections(IXPathProvider xpathProvider)
        {
            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 24 km W
                string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:[NSEW][NSEW\s\.-]{0,5}(?!\w)|(?i)(?:east|west|south|notrh)+))";
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    // Alert.Message(m.Value);
                    replace = Regex.Replace(replace, pattern, "<direction>$1</direction>");
                    node.InnerXml = replace;
                }
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagAltitude(IXPathProvider xpathProvider)
        {
            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 510–650 m a.s.l.
                string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:(?i)(?:<[^>]*>)*a\W*(?:<[^>]*>)*s\W*(?:<[^>]*>)*l\W?))";
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    // Alert.Message(m.Value);
                    replace = Regex.Replace(replace, pattern, "<altitude>$1</altitude>");
                    node.InnerXml = replace;
                }
            }

            this.ParseXmlDocumentToXmlString();
        }
    }
}
