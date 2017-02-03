namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Bio.Taxonomy;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Processors.Contracts.Providers;
    using ProcessingTools.Strings.Extensions;

    public class LowerTaxaInItalicTagger : ILowerTaxaInItalicTagger
    {
        private readonly ILowerTaxaDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly IPlausibleLowerTaxaHarvester plausibleLowerTaxaHarvester;
        private readonly ITaxaStopWordsProvider stopWordsProvider;
        private readonly IContentTagger contentTagger;

        public LowerTaxaInItalicTagger(
            ILowerTaxaDataMiner miner,
            ITextContentHarvester contentHarvester,
            IPlausibleLowerTaxaHarvester plausibleLowerTaxaHarvester,
            ITaxaStopWordsProvider stopWordsProvider,
            IContentTagger contentTagger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (plausibleLowerTaxaHarvester == null)
            {
                throw new ArgumentNullException(nameof(plausibleLowerTaxaHarvester));
            }

            if (stopWordsProvider == null)
            {
                throw new ArgumentNullException(nameof(stopWordsProvider));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.miner = miner;
            this.contentHarvester = contentHarvester;
            this.plausibleLowerTaxaHarvester = plausibleLowerTaxaHarvester;
            this.stopWordsProvider = stopWordsProvider;
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

            var stopWords = await this.stopWordsProvider.GetStopWords(document.XmlDocument.DocumentElement);

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var x = await this.miner.Mine(textContent, plausibleLowerTaxa, stopWords);


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
            var stopWords = await this.stopWordsProvider.GetStopWords(document.XmlDocument.DocumentElement);

            var taxaNamesFirstWord = await taxaNames.GetFirstWord()
                .Distinct()
                .DistinctWithStopWords(stopWords)
                .ToArrayAsync();

            return new HashSet<string>(taxaNamesFirstWord);
        }
    }
}
