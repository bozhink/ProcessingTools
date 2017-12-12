namespace ProcessingTools.Processors.Processors.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Contracts.Models.Processors.Abbreviations;
    using ProcessingTools.Contracts.Processors.Processors.Abbreviations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Models.Abbreviations;
    using ProcessingTools.Xml.Contracts.Wrappers;

    public class AbbreviationsTagger : IAbbreviationsTagger
    {
        private const string SelectNodesToTagAbbreviationsXPathTemplate = ".//node()[contains(string(.),string('{0}'))]";

        private readonly IAbbreviationsHarvester abbreviationsHarvester;
        private readonly IXmlContextWrapper contextWrapper;
        private readonly ILogger logger;

        public AbbreviationsTagger(
            IAbbreviationsHarvester abbreviationsHarvester,
            IXmlContextWrapper contextWrapper,
            ILogger logger)
        {
            this.abbreviationsHarvester = abbreviationsHarvester ?? throw new ArgumentNullException(nameof(abbreviationsHarvester));
            this.contextWrapper = contextWrapper ?? throw new ArgumentNullException(nameof(contextWrapper));
            this.logger = logger;
        }

        public async Task<object> TagAsync(XmlNode context)
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//graphic | //media | //disp-formula-group")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//chem-struct-wrap | //fig | //supplementary-material | //table-wrap")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//fig-group | //table-wrap-group")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(
                context,
                "//boxed-text")
                .ConfigureAwait(false);

            await this.TagAbbreviationsInSubContextSelectedByXPathWithHarvestContextAsync(
                context,
                "//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //funding-source | //license-p | //meta-value | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line")
                .ConfigureAwait(false);

            return true;
        }

        private async Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestSubContextAsync(XmlNode context, string selectContextToTagXPath)
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
                .Select(n => this.TagAbbreviationsWithHarvestWholeContextAsync(n))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task<object> TagAbbreviationsWithHarvestWholeContextAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var abbreviationDefinitions = await this.GetAbbreviationCollectionAsync(context).ConfigureAwait(false);
            var result = await this.TagAbbreviationsAsync(context, abbreviationDefinitions).ConfigureAwait(false);
            return result;
        }

        private async Task TagAbbreviationsInSubContextSelectedByXPathWithHarvestContextAsync(XmlNode context, string selectContextToTagXPath)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrWhiteSpace(selectContextToTagXPath))
            {
                throw new ArgumentNullException(nameof(selectContextToTagXPath));
            }

            var abbreviationDefinitions = await this.GetAbbreviationCollectionAsync(context).ConfigureAwait(false);

            var tasks = context.SelectNodes(selectContextToTagXPath)
                 .Cast<XmlNode>()
                 .Select(n => this.TagAbbreviationsAsync(n, abbreviationDefinitions))
                 .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task<object> TagAbbreviationsAsync(XmlNode context, IEnumerable<IAbbreviation> abbreviationDefinitions)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (abbreviationDefinitions == null || !abbreviationDefinitions.Any())
            {
                return 0;
            }

            return await Task.Run(() =>
            {
                var document = this.contextWrapper.Create(context);

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
        }

        private async Task<IEnumerable<IAbbreviation>> GetAbbreviationCollectionAsync(XmlNode contextToHarvest)
        {
            if (contextToHarvest == null)
            {
                throw new ArgumentNullException(nameof(contextToHarvest));
            }

            var abbreviations = await this.abbreviationsHarvester.HarvestAsync(contextToHarvest).ConfigureAwait(false);
            if (abbreviations != null)
            {
                var result = abbreviations
                    .Where(a => !string.IsNullOrWhiteSpace(a.Value))
                    .Where(a => !string.IsNullOrWhiteSpace(a.Definition))
                    .Where(a => a.Value.Length > 1)
                    .Select(a => new Abbreviation(a.Value, a.ContentType, a.Definition))
                    .ToList();

                return result;
            }

            return null;
        }
    }
}
