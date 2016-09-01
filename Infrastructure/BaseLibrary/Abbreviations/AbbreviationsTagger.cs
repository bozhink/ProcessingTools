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

        public async Task Tag()
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSpecificNodeByXPath("//graphic | //media | //disp-formula-group", ".//abbrev");
            await this.TagAbbreviationsInSpecificNodeByXPath("//chem-struct-wrap | //fig | //supplementary-material | //table-wrap", ".//abbrev");
            await this.TagAbbreviationsInSpecificNodeByXPath("//fig-group | //table-wrap-group", ".//abbrev");
            await this.TagAbbreviationsInSpecificNodeByXPath("//boxed-text", ".//abbrev");
            await this.TagAbbreviationsInSpecificNodeByXPath("//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //element-citation | //funding-source | //license-p | //meta-value | //mixed-citation | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line", "//abbrev");
        }

        private async Task TagAbbreviationsInSpecificNodeByXPath(string selectSpecificNodeXPath, string abbreviationsXpath)
        {
            var tasks = this.XmlDocument.SelectNodes(selectSpecificNodeXPath, this.NamespaceManager)
                .Cast<XmlNode>()
                .Select(n => this.TagAbbreviationsInSpecificNode(n, abbreviationsXpath))
                .ToArray();

            await Task.WhenAll(tasks);
        }

        private Task TagAbbreviationsInSpecificNode(XmlNode specificNode, string abbreviationsXpath) => Task.Run(() =>
        {
            var abbreviationList = new HashSet<IAbbreviation>(this.GetAbbreviationList(specificNode, abbreviationsXpath));

            foreach (var abbreviation in abbreviationList)
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
        });

        private IEnumerable<IAbbreviation> GetAbbreviationList(XmlNode specificNode, string abbreviationsXpath)
        {
            var abbreviationList = specificNode.SelectNodes(abbreviationsXpath, this.NamespaceManager)
                .Cast<XmlNode>()
                .Select(x => new Abbreviation(x))
                .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                .Where(a => !string.IsNullOrWhiteSpace(a.Definition))
                .Where(a => a.Content.Length > 1)
                .OrderByDescending(a => a.Content.Length)
                .ToList();

            return abbreviationList;
        }
    }
}