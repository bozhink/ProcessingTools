namespace ProcessingTools.BaseLibrary.Coordinates
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class CoordinatesTagger : TaggerBase, IBaseTagger
    {
        private const string LocalityCoordinatesTagName = "locality-coordinates";
        private readonly XmlElement localityCoordinatesNode = null;

        public CoordinatesTagger(Config config, string xml)
            : base(config, xml)
        {
            this.localityCoordinatesNode = this.XmlDocument.CreateElement(LocalityCoordinatesTagName);
        }

        public CoordinatesTagger(IBase baseObject)
            : base(baseObject)
        {
            this.localityCoordinatesNode = this.XmlDocument.CreateElement(LocalityCoordinatesTagName);
        }

        public void Tag()
        {
            string xml = this.Xml;

            xml = Regex.Replace(xml, @"(?<=\d)([º°˚]|<sup>o</sup>)", "°");

            XmlNode replacementNode = this.localityCoordinatesNode.CloneNode(true);
            replacementNode.InnerText = "$1";

            string replace = replacementNode.OuterXml;

            try
            {
                string xmlText = this.TextContent;

                List<string> coordinateStrings = new List<string>();

                {
                    Regex re = new Regex(@"((?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2})\s*?\W{0,3}?\s*(?:[NSEWO](?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*)(?:(?<!\d)[0-6]?[0-9](?:\s*[,\.]\s*\d+)?\W*?){0,2}))");

                    coordinateStrings.AddRange(xmlText.GetMatchesInText(re, false));
                }

                {
                    Regex re = new Regex(@"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?)\W{0,3}[NSEWO]))");

                    coordinateStrings.AddRange(xmlText.GetMatchesInText(re, false));
                }

                {
                    Regex re = new Regex(@"((?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO])\s*\W{0,3}?\s*(?:(?:(?<!\d)[0-1]?[0-9]{1,2}(?:\s*[,\.]\s*\d+)?\s*°\s*).{0,20}?[NSEWO]))");

                    coordinateStrings.AddRange(xmlText.GetMatchesInText(re, false));
                }

                {
                    Regex re = new Regex(@"((?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{3,6}\b)\s*[;,]\s*(?:[–—−-]?\s{0,2}\b[0-1]?[0-9]{1,2}[,\.][0-9]{3,6}\b))");

                    coordinateStrings.AddRange(xmlText.GetMatchesInText(re, false));
                }

                {
                    Regex re = new Regex(@"(?<latitude>(?:\b[–—−-]\W?|\b)[0-9]?[0-9]\b[\s\.]{1,3}[0-9]+)[^\w\.]{1,5}(?<longitude>(?:\b[–—−-]\W?|\b)(?:[0-1][0-8]|[0-9])?[0-9]\b[\s\.]{1,3}[0-9]+)");

                    coordinateStrings.AddRange(xmlText.GetMatchesInText(re, false));
                }

                foreach (string coordinateString in coordinateStrings)
                {
                    replacementNode.InnerText = coordinateString;

                    replacementNode.TagContentInDocument(this.XmlDocument.SelectNodes("/*"), true, true);
                }


            }
            catch
            {
                throw;
            }
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }
    }
}