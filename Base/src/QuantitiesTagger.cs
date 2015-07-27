using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class QuantitiesTagger : Base
    {
        public QuantitiesTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public QuantitiesTagger(Base baseObject)
            : base(baseObject)
        {
        }

        public void TagQuantities()
        {
            const string XPathTemplate = "//p[{0}]|//license-p[{0}]|//li[{0}]|//th[{0}]|//td[{0}]|//mixed-citation[{0}]|//element-citation[{0}]|//nlm-citation[{0}]|//tp:nomenclature-citation[{0}]";
            TagContent quantityTag = new TagContent("quantity");
            List<string> quantities = new List<string>();

            this.ParseXmlStringToXmlDocument();
            string xpath = string.Format(XPathTemplate, "normalize-space(.)!=''");
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

            // 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
            {
                string pattern = @"(?<!<[^>]+)((?:(?:[\(\)\[\]\{\}–—−‒-]\s*)??\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?(?:[kdcmµ]m|meters?|[kmµnp]g|[º°˚]\s*[FC]|[º°˚]|[M]?bp|ft|m|[kdcmµ]M|[dcmµ][lL]|[kdcmµ]mol|mile|mi|min(?:ute)|\%)\b)(?![^<>]*>)";
                Regex matchQuantities = new Regex(pattern);
                quantities = GetMatchesInXmlText(nodeList, matchQuantities, true);
            }

            foreach (string quantity in quantities)
            {
                Alert.Log(quantity);
                TagTextInXmlDocument(quantity, quantityTag, XPathTemplate, true);
            }

            this.ParseXmlDocumentToXmlString();
        }

        public void TagDirections()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
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

        public void TagAltitude()
        {
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            foreach (XmlNode node in this.xmlDocument.SelectNodes(xpath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 510–650 m a.s.l.
                string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:(?i)(?:<[^>]*>)*a\W*<[^>]*>)*s\W*<[^>]*>)*l\W?))";
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
