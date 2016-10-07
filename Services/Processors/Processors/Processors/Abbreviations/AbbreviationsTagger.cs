namespace ProcessingTools.Processors.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models.Abbreviations;

    using ProcessingTools.Contracts;
    using ProcessingTools.Harvesters.Contracts;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class AbbreviationsTagger : IAbbreviationsTagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[contains(string(.),string('{0}'))]";

        private readonly IAbbreviationsHarvester abbreviationsHarvester;
        private readonly IXmlContextWrapperProvider wrapperProvider;
        private readonly ILogger logger;

        public AbbreviationsTagger(
            IAbbreviationsHarvester abbreviationsHarvester,
            IXmlContextWrapperProvider wrapperProvider,
            ILogger logger)
        {
            if (abbreviationsHarvester == null)
            {
                throw new ArgumentNullException(nameof(abbreviationsHarvester));
            }

            if (wrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(wrapperProvider));
            }

            this.abbreviationsHarvester = abbreviationsHarvester;
            this.wrapperProvider = wrapperProvider;
            this.logger = logger;
        }

        public async Task<object> Tag(XmlNode context)
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContext(
                context,
                "//graphic | //media | //disp-formula-group");

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContext(
                context,
                "//chem-struct-wrap | //fig | //supplementary-material | //table-wrap");

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContext(
                context,
                "//fig-group | //table-wrap-group");

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContext(
                context,
                "//boxed-text");

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestContext(
                context,
                "//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //funding-source | //license-p | //meta-value | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line");

            return true;
        }

        private async Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContext(XmlNode context, string selectContextToTagXPath)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrWhiteSpace(selectContextToTagXPath))
            {
                throw new ArgumentNullException(nameof(selectContextToTagXPath));
            }

            var tasks = context.SelectNodes(selectContextToTagXPath)
                .Cast<XmlNode>()
                .Select(n => this.TagAbbreviationsWithHarvestWholeContext(n))
                .ToArray();

            await Task.WhenAll(tasks);
        }

        private async Task<object> TagAbbreviationsWithHarvestWholeContext(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var abbreviationDefinitions = await this.GetAbbreviationCollection(context);
            var result = await this.TagAbbreviations(context, abbreviationDefinitions);
            return result;
        }

        private async Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestContext(XmlNode context, string selectContextToTagXPath)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrWhiteSpace(selectContextToTagXPath))
            {
                throw new ArgumentNullException(nameof(selectContextToTagXPath));
            }

            var abbreviationDefinitions = await this.GetAbbreviationCollection(context);

            var tasks = context.SelectNodes(selectContextToTagXPath)
                 .Cast<XmlNode>()
                 .Select(n => this.TagAbbreviations(n, abbreviationDefinitions))
                 .ToArray();

            await Task.WhenAll(tasks);
        }

        private Task<object> TagAbbreviations(XmlNode context, IQueryable<IAbbreviation> abbreviationDefinitions) => Task.Run<object>(() =>
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (abbreviationDefinitions == null || abbreviationDefinitions.LongCount() < 1L)
            {
                return 0;
            }

            var document = this.wrapperProvider.Create(context);

            var abbreviationSet = new HashSet<IAbbreviation>(abbreviationDefinitions);
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

            context.InnerXml = document.DocumentElement.InnerXml;

            return abbreviationSet.Count;
        });

        private async Task<IQueryable<IAbbreviation>> GetAbbreviationCollection(XmlNode contextToHarvest)
        {
            if (contextToHarvest == null)
            {
                throw new ArgumentNullException(nameof(contextToHarvest));
            }

            var abbreviations = await this.abbreviationsHarvester.Harvest(contextToHarvest);
            if (abbreviations != null)
            {
                return abbreviations
                    .Where(a => !string.IsNullOrWhiteSpace(a.Value))
                    .Where(a => !string.IsNullOrWhiteSpace(a.Definition))
                    .Where(a => a.Value.Length > 1)
                    .Select(a => new Abbreviation(a.Value, a.ContentType, a.Definition));
            }

            return null;
        }
    }
}
