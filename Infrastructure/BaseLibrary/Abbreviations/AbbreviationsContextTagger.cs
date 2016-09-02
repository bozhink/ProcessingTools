namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Contract;
    using System.Collections.Generic;
    using System.Linq;

    public class AbbreviationsContextTagger : IAbbreviationsContextTagger
    {
        private const string AbbreviationXpath = "//abbrev";
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),string('{0}'))][count(.//node()[contains(string(.),string('{0}'))])=0]";

        private readonly IXmlContextWrapperProvider wrapperProvider;
        private readonly ILogger logger;

        public AbbreviationsContextTagger(IXmlContextWrapperProvider wrapperProvider, ILogger logger)
        {
            if (wrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(wrapperProvider));
            }

            this.wrapperProvider = wrapperProvider;
            this.logger = logger;
        }

        public Task<object> Tag(XmlNode context) => Task.Run<object>(() =>
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var i = context.InnerXml.Length;

            var document = this.wrapperProvider.Create(context);

            var abbreviationList = new HashSet<IAbbreviation>(this.GetAbbreviationList(document));

            foreach (var abbreviation in abbreviationList)
            {
                string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                foreach (XmlNode node in document.SelectNodes(xpath))
                {
                    bool performReplace = node.CheckIfIsPossibleToPerformReplaceInXmlNode();
                    if (performReplace)
                    {
                        try
                        {
                            node.ReplaceWholeXmlNodeByRegexPattern(abbreviation.SearchPattern, abbreviation.ReplacePattern);
                        }
                        catch (XmlException)
                        {
                            this.logger?.Log("Exception in abbreviation {0}", abbreviation.Content);
                        }
                    }
                }
            }

            context.InnerXml = document.DocumentElement.InnerXml;
            Console.WriteLine("{0} {1}\n\n", i, context.InnerXml.Length);

            return abbreviationList.Count;
        });

        private IEnumerable<IAbbreviation> GetAbbreviationList(XmlNode context)
        {
            var abbreviationList = context.SelectNodes(AbbreviationXpath)
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
