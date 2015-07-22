using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
    public class QuantitiesTagger : Base
    {
        public QuantitiesTagger()
            : base()
        {
        }

        public QuantitiesTagger(string xml)
            : base(xml)
        {
        }

        public QuantitiesTagger(Config config)
            : base(config)
        {
        }

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
            string xpath = "//p|//license-p|//li|//th|//td|//mixed-citation|//element-citation|//nlm-citation|//tp:nomenclature-citation";

            this.ParseXmlStringToXmlDocument();
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

            // 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
            string pattern = @"(?<!<[^>]+)((?:(?:[\(\)\[\]\{\}–—−‒-]\s*)?\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+(?:[kdcmµ]m|meters?|[º°˚]\s*[FC]|bp|ft|m|[kdcmµ]M|[dcmµ][lL]|[kdcmµ]mol|mile|mi|min(?:ute)|\%)\b)(?![^<>]*>)";

            foreach (XmlNode node in nodeList)
            {
                string replace = node.InnerXml;
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    // Alert.Message(m.Value);
                    replace = Regex.Replace(replace, pattern, "<quantity>$1</quantity>");
                    node.InnerXml = replace;
                }
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
    }
}
