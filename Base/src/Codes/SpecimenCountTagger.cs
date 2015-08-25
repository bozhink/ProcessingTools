using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

/*
 * 1 male, 1 female
 * 2 spec.
 * 2 exx.
 * 2 spp.
 * 1 ex.
 */


namespace ProcessingTools.Base
{
    public class SpecimenCountTagger : TaggerBase
    {
        private TagContent specimenCountTag = new TagContent("specimen-count");

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
            const string pattern = @"(?<!<[^>]+)((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)(?![^<>]*>)";
            Regex matchSpecimenCount = new Regex(pattern);
            List<string> specimenCountCitations = this.TextContent.GetMatchesInText(matchSpecimenCount, true);
            TagTextInXmlDocument(specimenCountCitations, specimenCountTag, xpathProvider.SelectContentNodesXPathTemplate, false, true);


            //string replacement = specimenCountTag.OpenTag + "$1" + specimenCountTag.CloseTag;
            //foreach (XmlNode node in this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPath, this.NamespaceManager))
            //{
            //    string replace = node.InnerXml;
            //    {
                    
                    
            //        if (matchSpecimenCount.Match(replace).Success)
            //        {
            //            replace = matchSpecimenCount.Replace(replace, replacement);
            //            node.InnerXml = replace;
            //        }
            //    }
            //}
        }
    }
}
