namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts;

    public class AbbreviationsTagger : HarvestableDocument, ITagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),string('{0}'))][count(.//node()[contains(string(.),string('{0}'))])=0]";

        public AbbreviationsTagger(Config config, string xml)
            : base(config, xml)
        {
        }

        public AbbreviationsTagger(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Tag()
        {
            // Do not change this sequence
            this.TagAbbreviationsInSpecificNodeByXPath("//graphic|//media|//disp-formula-group");
            this.TagAbbreviationsInSpecificNodeByXPath("//chem-struct-wrap|//fig|//supplementary-material|//table-wrap");
            this.TagAbbreviationsInSpecificNodeByXPath("//fig-group|//table-wrap-group");
            this.TagAbbreviationsInSpecificNodeByXPath("//boxed-text");
            this.TagAbbreviationsInSpecificNodeByXPath("/");
        }

        private void TagAbbreviationsInSpecificNode(XmlNode specificNode)
        {
            HashSet<Abbreviation> abbreviationsList = new HashSet<Abbreviation>(specificNode.SelectNodes(".//abbrev", this.NamespaceManager)
                    .Cast<XmlNode>().Select(x => new Abbreviation(x)));

            foreach (Abbreviation abbreviation in abbreviationsList)
            {
                string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                foreach (XmlNode nodeInSpecificNode in specificNode.SelectNodes(xpath, this.NamespaceManager))
                {
                    bool performReplace = nodeInSpecificNode.CheckIfIsPossibleToPerformReplaceInXmlNode();
                    if (performReplace)
                    {
                        nodeInSpecificNode.ReplaceWholeXmlNodeByRegexPattern(abbreviation.SearchPattern, abbreviation.ReplacePattern);
                    }
                }
            }
        }

        private void TagAbbreviationsInSpecificNodeByXPath(string selectSpecificNodeXPath)
        {
            XmlNodeList specificNodes = this.XmlDocument.SelectNodes(selectSpecificNodeXPath, this.NamespaceManager);
            foreach (XmlNode specificNode in specificNodes)
            {
                this.TagAbbreviationsInSpecificNode(specificNode);
            }
        }
    }
}