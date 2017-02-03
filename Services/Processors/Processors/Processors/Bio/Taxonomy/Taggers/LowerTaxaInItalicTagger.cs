namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio.Taxonomy;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Strings.Extensions;

    public class LowerTaxaInItalicTagger : ILowerTaxaInItalicTagger
    {
        private readonly IPlausibleLowerTaxaHarvester plausibleLowerTaxaHarvester;
        private readonly ITaxaStopWordsProvider taxaStopWordsProvider;
        private readonly IContentTagger contentTagger;

        public LowerTaxaInItalicTagger(
            IPlausibleLowerTaxaHarvester plausibleLowerTaxaHarvester,
            ITaxaStopWordsProvider taxaStopWordsProvider,
            IContentTagger contentTagger)
        {
            if (plausibleLowerTaxaHarvester == null)
            {
                throw new ArgumentNullException(nameof(plausibleLowerTaxaHarvester));
            }

            if (taxaStopWordsProvider == null)
            {
                throw new ArgumentNullException(nameof(taxaStopWordsProvider));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.plausibleLowerTaxaHarvester = plausibleLowerTaxaHarvester;
            this.taxaStopWordsProvider = taxaStopWordsProvider;
            this.contentTagger = contentTagger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var plausibleLowerTaxa = await this.plausibleLowerTaxaHarvester.Harvest(document.XmlDocument.DocumentElement);
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

        private async Task<IEnumerable<string>> ClearFakeTaxaNames(IDocument document, IEnumerable<string> taxaNames)
        {
            var taxaNamesFirstWord = await this.GetTaxaNamesFirstWord(document, taxaNames);

            var result = taxaNames.Where(tn => taxaNamesFirstWord.Contains(tn.GetFirstWord()));
            return result;
        }

        private async Task<IEnumerable<string>> GetTaxaNamesFirstWord(IDocument document, IEnumerable<string> taxaNames)
        {
            var stopWords = await this.taxaStopWordsProvider.GetStopWords(document.XmlDocument.DocumentElement);

            var taxaNamesFirstWord = await taxaNames.GetFirstWord()
                .Distinct()
                .DistinctWithStopWords(stopWords)
                .ToArrayAsync();

            return new HashSet<string>(taxaNamesFirstWord);
        }
    }
}
