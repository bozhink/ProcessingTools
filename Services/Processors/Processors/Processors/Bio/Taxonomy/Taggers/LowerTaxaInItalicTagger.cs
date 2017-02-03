namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio.Taxonomy;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Meta;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Strings.Extensions;

    public class LowerTaxaInItalicTagger : ILowerTaxaInItalicTagger
    {
        private readonly ILowerTaxaInItalicHarvester lowerTaxaInItalicHarvester;
        private readonly IPersonNamesHarvester personNamesHarvester;
        private readonly IContentTagger contentTagger;
        private readonly IBlackList blacklist;
        private readonly ILogger logger;

        public LowerTaxaInItalicTagger(
            ILowerTaxaInItalicHarvester lowerTaxaInItalicHarvester,
            IPersonNamesHarvester personNamesHarvester,
            IBlackList blacklist,
            IContentTagger contentTagger,
            ILogger logger)
        {
            if (lowerTaxaInItalicHarvester == null)
            {
                throw new ArgumentNullException(nameof(lowerTaxaInItalicHarvester));
            }

            if (personNamesHarvester == null)
            {
                throw new ArgumentNullException(nameof(personNamesHarvester));
            }

            if (blacklist == null)
            {
                throw new ArgumentNullException(nameof(blacklist));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.lowerTaxaInItalicHarvester = lowerTaxaInItalicHarvester;
            this.personNamesHarvester = personNamesHarvester;
            this.blacklist = blacklist;
            this.contentTagger = contentTagger;
            this.logger = logger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var knownLowerTaxaNames = this.GetKnownLowerTaxa(document);

            var plausibleLowerTaxaInItelics = await this.lowerTaxaInItalicHarvester.Harvest(document.XmlDocument.DocumentElement);
            var plausibleLowerTaxa = new HashSet<string>(plausibleLowerTaxaInItelics.Concat(knownLowerTaxaNames));

            plausibleLowerTaxa = new HashSet<string>((await this.ClearFakeTaxaNames(document, plausibleLowerTaxa))
                .Select(name => name.ToLower()));

            this.TagDirectTaxonomicMatches(document, plausibleLowerTaxa);

            return true;
        }

        private void TagDirectTaxonomicMatches(IDocument document, IEnumerable<string> taxonomicNames)
        {
            var comparer = StringComparer.InvariantCultureIgnoreCase;

            // Tag all direct matches
            document.SelectNodes(XPathStrings.ItalicsForLowerTaxaHarvesting)
                .AsParallel()
                .ForAll(node =>
                {
                    if (taxonomicNames.Contains(node.InnerText, comparer))
                    {
                        var tn = document.CreateTaxonNameXmlElement(TaxonType.Lower);
                        tn.InnerXml = node.InnerXml
                            .RegexReplace(@"\A([A-Z][A-Za-z]{0,2}\.)([a-z])", "$1 $2");
                        node.InnerXml = tn.OuterXml;
                    }
                });
        }

        private IEnumerable<string> GetKnownLowerTaxa(IDocument document)
        {
            var result = document.SelectNodes(".//tn[@type='lower']")
                .Select(x => x.InnerText)
                .Distinct()
                .ToArray();

            return result;
        }

        private async Task<IEnumerable<string>> ClearFakeTaxaNames(IDocument document, IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWord(document, taxaNames);

            var result = taxaNames.Where(tn => taxaNamesFirstWord.Contains(tn.GetFirstWord()));
            return result;
        }

        private async Task<IEnumerable<string>> GetTaxaNamesFirstWord(IDocument document, IEnumerable<string> taxaNames)
        {
            var stopWords = await this.GetStopWords(document.XmlDocument.DocumentElement);

            var taxaNamesFirstWord = await taxaNames.GetFirstWord()
                .Distinct()
                .DistinctWithStopWords(stopWords)
                .ToArrayAsync();

            return new HashSet<string>(taxaNamesFirstWord);
        }

        private async Task<IEnumerable<string>> GetStopWords(XmlNode context)
        {
            var personNames = await this.personNamesHarvester.Harvest(context);
            var blacklistItems = await this.blacklist.Items;

            var stopWords = await personNames
                .SelectMany(n => new string[] { n.GivenNames, n.Surname, n.Suffix, n.Prefix })
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Union(blacklistItems)
                .Select(w => w.ToLower())
                .Distinct()
                .ToArrayAsync();

            return stopWords;
        }
    }
}
