using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class SpecimenCountTagger : TaggerBase
    {
        private const string TagName = "specimen-count";
        private const string TagReplacement = "<" + TagName + ">$1</" + TagName + ">";

        public SpecimenCountTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public SpecimenCountTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void TagSpecimenCount(IXPathProvider xpathProvider)
        {
            this.ParseXmlStringToXmlDocument();

            foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            {
                string replace = node.InnerXml;

                // 1♀
                {
                    string pattern = @"(?<!<[^>]+)((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)(?![^<>]*>)";
                    Regex matchSpecimenCount = new Regex(pattern);
                    if (matchSpecimenCount.Match(replace).Success)
                    {
                        replace = matchSpecimenCount.Replace(replace, TagReplacement);
                        node.InnerXml = replace;
                    }
                }
            }

            this.ParseXmlDocumentToXmlString();
        }
    }
}
