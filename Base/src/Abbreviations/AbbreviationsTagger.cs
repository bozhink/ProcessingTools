namespace ProcessingTools.Base.Abbreviations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public class AbbreviationsTagger : TaggerBase, IBaseTagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = "//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),'{0}')][count(.//node()[contains(string(.),'{0}')])=0]";

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

        public void Tag(IXPathProvider xpathProvider)
        {
            this.Tag();
        }

        private Abbreviation ConvertAbbrevXmlNodeToAbbreviation(XmlNode abbrev)
        {
            Abbreviation abbreviation = new Abbreviation();

            abbreviation.Content = Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                abbrev.InnerXml,
                                @"<def.+</def>",
                                string.Empty),
                            @"<def[*>]</def>|</?b[^>]*>",
                            string.Empty),
                        @"\A\W+|\W+\Z",
                        string.Empty);

            if (abbrev.Attributes["content-type"] != null)
            {
                abbreviation.ContentType = abbrev.Attributes["content-type"].InnerText;
            }

            if (abbrev["def"] != null)
            {
                abbreviation.Definition = Regex.Replace(
                    Regex.Replace(
                        abbrev["def"].InnerXml,
                        "<[^>]*>",
                        string.Empty),
                    @"\A[=,;:\s–—−-]+|[=,;:\s–—−-]+\Z|\s+(?=\s)",
                    string.Empty);
            }

            return abbreviation;
        }

        private void TagAbbreviationsInSpecificNode(XmlNode specificNode)
        {
            List<Abbreviation> abbreviationsList = specificNode.SelectNodes(".//abbrev", this.NamespaceManager)
                                .Cast<XmlNode>().Select(a => this.ConvertAbbrevXmlNodeToAbbreviation(a)).ToList<Abbreviation>();

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