namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Contract;

    public class AbbreviationsTagger : IAbbreviationsTagger
    {
        private const string AbbreviationXpath = ".//abbrev";
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[count(ancestor-or-self::node()[name()='abbrev'])=0][contains(string(.),string('{0}'))][count(.//node()[contains(string(.),string('{0}'))])=0]";

        private readonly IXmlContextWrapperProvider wrapperProvider;
        private readonly ILogger logger;

        public AbbreviationsTagger(IXmlContextWrapperProvider wrapperProvider, ILogger logger)
        {
            if (wrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(wrapperProvider));
            }

            this.wrapperProvider = wrapperProvider;
            this.logger = logger;
        }

        public async Task<object> Tag(XmlNode context)
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSubContextSelectedByXPath(
                context,
                "//graphic | //media | //disp-formula-group");

            await this.TagAbbreviationsInSubContextSelectedByXPath(
                context,
                "//chem-struct-wrap | //fig | //supplementary-material | //table-wrap");

            await this.TagAbbreviationsInSubContextSelectedByXPath(
                context,
                "//fig-group | //table-wrap-group");

            await this.TagAbbreviationsInSubContextSelectedByXPath(
                context,
                "//boxed-text");

            await this.TagAbbreviationsInSubContextSelectedByXPath(
                context,
                "//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //element-citation | //funding-source | //license-p | //meta-value | //mixed-citation | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line",
                context);

            return true;
        }

        /// <summary>
        /// Splits the <paramref name="context"/> into sub-contexts using XPath selector and tag them in parallel.
        /// Gets definitions of abbreviations from the <paramref name="contextToHarvest"/>.
        /// If <paramref name="contextToHarvest"/> is null, current sub-context will be harvested.
        /// </summary>
        /// <param name="context">Context to be tagged in parallel.</param>
        /// <param name="selectContextToTagXPath">XPath selector to select independent sub-contexts to be tagged in parallel.</param>
        /// <param name="contextToHarvest">The context to be harvested for abbreviation definitions.</param>
        /// <returns>Task to be awaited.</returns>
        private async Task TagAbbreviationsInSubContextSelectedByXPath(XmlNode context, string selectContextToTagXPath, XmlNode contextToHarvest = null)
        {
            var tasks = context.SelectNodes(selectContextToTagXPath)
                .Cast<XmlNode>()
                .Select(n => this.TagAbbreviations(n, contextToHarvest))
                .ToArray();

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Tags abbreviations in contextToTag.
        /// Gets definitions of abbreviations from the <paramref name="contextToHarvest"/>.
        /// If <paramref name="contextToHarvest"/> is null, current context will be harvested.
        /// </summary>
        /// <param name="contextToTag">Context to be tagged.</param>
        /// <param name="contextToHarvest">The context to be harvested for abbreviation definitions.</param>
        /// <returns>Task of number of unique abbreviations in current <paramref name="contextToHarvest"/>.</returns>
        private async Task<object> TagAbbreviations(XmlNode contextToTag, XmlNode contextToHarvest = null)
        {
            if (contextToTag == null)
            {
                throw new ArgumentNullException(nameof(contextToTag));
            }

            var document = this.wrapperProvider.Create(contextToTag);

            // If context to harvest is null use the contextToTag instead
            var abbreviationCollection = await this.GetAbbreviationCollection(contextToHarvest ?? contextToTag);

            var abbreviationSet = new HashSet<IAbbreviation>(abbreviationCollection);
            abbreviationSet.OrderByDescending(a => a.Content.Length)
                .ToList()
                .ForEach(abbreviation =>
                {
                    string xpath = string.Format(SelectNodesToTagAbbreviationsXPathTemplate, abbreviation.Content);
                    foreach (XmlNode node in document.SelectNodes(xpath))
                    {
                        bool canPerformReplace = node.CheckIfIsPossibleToPerformReplaceInXmlNode();
                        if (canPerformReplace)
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
                });

            contextToTag.InnerXml = document.DocumentElement.InnerXml;

            return abbreviationSet.Count;
        }

        private Task<IEnumerable<IAbbreviation>> GetAbbreviationCollection(XmlNode context) => Task.Run(() =>
        {
            var abbreviationList = context.SelectNodes(AbbreviationXpath)
                .Cast<XmlNode>()
                .Select(x => new Abbreviation(x))
                .Where(a => !string.IsNullOrWhiteSpace(a.Content))
                .Where(a => !string.IsNullOrWhiteSpace(a.Definition))
                .Where(a => a.Content.Length > 1)
                .ToList<IAbbreviation>();

            return abbreviationList.AsEnumerable();
        });
    }
}
