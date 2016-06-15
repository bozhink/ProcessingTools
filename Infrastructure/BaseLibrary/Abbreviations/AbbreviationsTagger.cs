namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public class AbbreviationsTagger : TaxPubDocument, ITagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),string('{0}'))][count(.//node()[contains(string(.),string('{0}'))])=0]";

        public AbbreviationsTagger(string xml)
            : base(xml)
        {
        }

        public ILogger Logger { get; set; }

        public Task Tag()
        {
            return Task.Run(() =>
            {
                // Do not change this sequence
                this.TagAbbreviationsInSpecificNodeByXPath("//graphic | //media | //disp-formula-group", ".//abbrev");
                this.TagAbbreviationsInSpecificNodeByXPath("//chem-struct-wrap | //fig | //supplementary-material | //table-wrap", ".//abbrev");
                this.TagAbbreviationsInSpecificNodeByXPath("//fig-group | //table-wrap-group", ".//abbrev");
                this.TagAbbreviationsInSpecificNodeByXPath("//boxed-text", ".//abbrev");
                this.TagAbbreviationsInSpecificNodeByXPath("//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //element-citation | //funding-source | //license-p | //meta-value | //mixed-citation | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line", "//abbrev");
            });
        }

        private void TagAbbreviationsInSpecificNode(XmlNode specificNode, string abbreviationsXpath)
        {
            var abbreviationsList = new HashSet<Abbreviation>(specificNode
                .SelectNodes(abbreviationsXpath, this.NamespaceManager)
                .Cast<XmlNode>()
                .Select(x => new Abbreviation(x))
                .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                .Where(a => !string.IsNullOrWhiteSpace(a.Definition))
                .OrderByDescending(a => a.Content.Length)
                .ToList());

            foreach (Abbreviation abbreviation in abbreviationsList)
            {
                string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                foreach (XmlNode nodeInSpecificNode in specificNode.SelectNodes(xpath, this.NamespaceManager))
                {
                    bool performReplace = nodeInSpecificNode.CheckIfIsPossibleToPerformReplaceInXmlNode();
                    if (performReplace)
                    {
                        try
                        {
                            nodeInSpecificNode.ReplaceWholeXmlNodeByRegexPattern(abbreviation.SearchPattern, abbreviation.ReplacePattern);
                        }
                        catch (XmlException)
                        {
                            this.Logger?.Log("Exception in abbreviation {0}", abbreviation.Content);
                        }
                    }
                }
            }
        }

        private void TagAbbreviationsInSpecificNodeByXPath(string selectSpecificNodeXPath, string abbreviationsXpath)
        {
            XmlNodeList specificNodes = this.XmlDocument.SelectNodes(selectSpecificNodeXPath, this.NamespaceManager);
            foreach (XmlNode specificNode in specificNodes)
            {
                this.TagAbbreviationsInSpecificNode(specificNode, abbreviationsXpath);
            }
        }
    }
}