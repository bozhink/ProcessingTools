namespace ProcessingTools.BaseLibrary.Measurements
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Globals;

    public class QuantitiesTagger : TaggerBase
    {
        private TagContent quantityTag = new TagContent("quantity");
        private TagContent deviationTag = new TagContent("deviation");
        private TagContent altitudeTag = new TagContent("altitude");

        private ILogger logger;

        public QuantitiesTagger(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public QuantitiesTagger(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        /*
         * ~16–~61m
         * 28.4–30.0 °C
         * * 30.1–31.2 ppt
         * 1,500 m × 200 m
         * * 15 mM
         * 15 units/ µl
         * 30 s
         * 5 min
         * 8 minutes
         * between 432 and 887 bp
         * 0.6–1.9 mm, 1.1–1.7 × 0.5–0.8 mm
         * 3.5 cm × 3 cm
         * 11 cm x 8 cm
         * * 59 kV
         * * 167 µA
         * 2–4 mm
         * 2.2–2.6 mm
         */
        public void TagQuantities(IXPathProvider xpathProvider)
        {
            string pattern = @"(?<!<[^>]+)((?:(?:[\(\)\[\]\{\}–—−‒-]\s*)??\d+(?:[,\.]\d+)?(?:\s*[\(\)\[\]\{\}×\*])?\s*)+?(?:[kdcmµnp][gmMlLVA]|[kdcmµ]mol|meters?|[º°˚]\s*[FC]|[M]?bp|ppt|fe*t|m|mi(?:le)|min(?:ute))\b)(?![^<>]*>)";
            Regex matchQuantities = new Regex(pattern);
            IEnumerable<string> quantities = this.TextContent.GetMatchesInText(matchQuantities);
            foreach (string quantity in quantities)
            {
                this.logger?.Log(quantity);
                quantity.TagContentInDocument(this.quantityTag, xpathProvider.SelectContentNodesXPathTemplate, this.XmlDocument, true);
            }
        }

        public void TagDeviation(IXPathProvider xpathProvider)
        {
            /*
             * Deviation:
             * -36.5806 149.3153 ±100 m
             */

            string replacement = this.deviationTag.OpenTag + "$1" + this.deviationTag.CloseTag;
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                //// 24 km W
                string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:[NSEW][NSEW\s\.-]{0,5}(?!\w)|(?i)(?:east|west|south|notrh)+))";
                Match m = Regex.Match(replace, pattern);
                if (m.Success)
                {
                    replace = Regex.Replace(replace, pattern, replacement);
                    node.InnerXml = replace;
                }
            }
        }

        ////  6–8 m depth
        public void TagAltitude(IXPathProvider xpathProvider)
        {
            string replacement = this.altitudeTag.OpenTag + "$1" + this.altitudeTag.CloseTag;
            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                {
                    //// 510–650 m a.s.l.
                    string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:(?i)(?:<[^>]*>)*a\W*(?:<[^>]*>)*s\W*(?:<[^>]*>)*l[^\w<>]?))";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        replace = Regex.Replace(replace, pattern, replacement);
                        node.InnerXml = replace;
                    }
                }

                {
                    //// 58 m alt.
                    string pattern = @"(<quantity>.*?</quantity>\W{0,4}(?:(?i)(?:<[^>]*>)*a(?:<[^>]*>)*l(?:<[^>]*>)*t[^\w<>]?))";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        replace = Regex.Replace(replace, pattern, replacement);
                        node.InnerXml = replace;
                    }
                }

                {
                    //// alt. ca. 271 m
                    string pattern = @"((?:(?i)(?:<[^>]*>)*a(?:<[^>]*>)*l(?:<[^>]*>)*t(?:<[^>]*>)*(?:[^\w<>]{0,3}c(?:<[^>]*>)*a(?:<[^>]*>)*)?)[^\w<>]{0,5}<quantity>.*?</quantity>)";
                    Match m = Regex.Match(replace, pattern);
                    if (m.Success)
                    {
                        replace = Regex.Replace(replace, pattern, replacement);
                        node.InnerXml = replace;
                    }
                }
            }
        }
    }
}
