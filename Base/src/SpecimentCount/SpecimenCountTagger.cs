/*
 * 1 male, 1 female
 * 2 spec.
 * 2 exx.
 * 2 spp.
 * 1 ex.
 * 1♂&amp;1♀
 */

namespace ProcessingTools.BaseLibrary
{
    using System.Text.RegularExpressions;
    using Configurator;
    using Globals;
    using Globals.Loggers;

    public class SpecimenCountTagger : TaggerBase
    {
        private TagContent specimenCountTag = new TagContent("specimen-count");

        private ILogger logger;

        public SpecimenCountTagger(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public SpecimenCountTagger(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void TagSpecimenCount(IXPathProvider xpathProvider)
        {
            string pattern = @"(?<!<[^>]+)((?i)(?:\d+(?:\s*[–—−‒-]?\s*))+[^\w<>\(\)\[\]]{0,5}(?:[♀♂]|males?|females?|juveniles?)+)(?![^<>]*>)";
            Regex matchSpecimenCount = new Regex(pattern);

            var nodeList = this.XmlDocument.SelectNodes(xpathProvider.SelectContentNodesXPathTemplate, this.NamespaceManager);
            var specimenCountCitations = this.TextContent.GetMatchesInText(matchSpecimenCount);
            specimenCountCitations.TagContentInDocument(this.specimenCountTag, nodeList, false, true, this.logger);
        }
    }
}
