namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Content;
    using ProcessingTools.Contracts.Harvesters.Meta;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class HigherTaxaTagger : IHigherTaxaTagger
    {
        private const string HigherTaxaXPath = ".//p|.//td|.//th|.//li|.//article-title|.//title|.//label|.//ref|.//kwd|.//tp:nomenclature-citation|.//*[@object_id='95']|.//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48']";

        private readonly IHigherTaxaDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly IPersonNamesHarvester personNamesHarvester;
        private readonly IBlackList blacklist;
        private readonly IWhiteList whitelist;
        private readonly IStringTagger contentTagger;

        public HigherTaxaTagger(
            IHigherTaxaDataMiner miner,
            ITextContentHarvester contentHarvester,
            IPersonNamesHarvester personNamesHarvester,
            IBlackList blacklist,
            IWhiteList whitelist,
            IStringTagger contentTagger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.personNamesHarvester = personNamesHarvester ?? throw new ArgumentNullException(nameof(personNamesHarvester));
            this.blacklist = blacklist ?? throw new ArgumentNullException(nameof(blacklist));
            this.whitelist = whitelist ?? throw new ArgumentNullException(nameof(whitelist));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement);
            var stopWords = await this.GetStopWords(context.XmlDocument.DocumentElement);
            var seed = await this.whitelist.GetItemsAsync();

            var data = await this.miner.MineAsync(textContent, seed, stopWords).ConfigureAwait(false) ?? new string[] { };

            var taxaNames = new HashSet<string>(data.Where(s => s != null && s.Length > 0 && s[0] == s.ToUpperInvariant()[0]));

            var tagModel = context.CreateTaxonNameXmlElement(TaxonType.Higher);
            await this.contentTagger.Tag(context, taxaNames, tagModel, HigherTaxaXPath).ConfigureAwait(false);

            return true;
        }

        private async Task<IEnumerable<string>> GetStopWords(XmlNode context)
        {
            var personNames = await this.personNamesHarvester.HarvestAsync(context);
            var blacklistItems = await this.blacklist.GetItemsAsync();

            var stopWords = personNames
                .SelectMany(n => new[] { n.GivenNames, n.Surname, n.Suffix, n.Prefix })
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Union(blacklistItems)
                .Distinct()
                .ToArray();

            return stopWords;
        }
    }
}
