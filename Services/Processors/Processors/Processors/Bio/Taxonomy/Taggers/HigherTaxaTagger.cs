namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Meta;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

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
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (personNamesHarvester == null)
            {
                throw new ArgumentNullException(nameof(personNamesHarvester));
            }

            if (blacklist == null)
            {
                throw new ArgumentNullException(nameof(blacklist));
            }

            if (whitelist == null)
            {
                throw new ArgumentNullException(nameof(whitelist));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.miner = miner;
            this.contentHarvester = contentHarvester;
            this.personNamesHarvester = personNamesHarvester;
            this.blacklist = blacklist;
            this.whitelist = whitelist;
            this.contentTagger = contentTagger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var stopWords = await this.GetStopWords(document.XmlDocument.DocumentElement);
            var seed = await this.whitelist.Items;

            var data = await this.miner.Mine(textContent, seed, stopWords);

            var taxaNames = new HashSet<string>(data.Where(s => s[0] == s.ToUpper()[0]));

            var tagModel = document.CreateTaxonNameXmlElement(TaxonType.Higher);
            await this.contentTagger.Tag(document, taxaNames, tagModel, HigherTaxaXPath);

            return true;
        }

        private async Task<IEnumerable<string>> GetStopWords(XmlNode context)
        {
            var personNames = await this.personNamesHarvester.Harvest(context);
            var blacklistItems = await this.blacklist.Items;

            var stopWords = await personNames
                .SelectMany(n => new string[] { n.GivenNames, n.Surname, n.Suffix, n.Prefix })
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Union(blacklistItems)
                .Distinct()
                .ToArrayAsync();

            return stopWords;
        }
    }
}
