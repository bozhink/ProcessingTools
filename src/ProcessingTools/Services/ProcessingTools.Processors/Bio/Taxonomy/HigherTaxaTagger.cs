// <copyright file="HigherTaxaTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Higher taxa tagger.
    /// </summary>
    public class HigherTaxaTagger : IHigherTaxaTagger
    {
        private const string HigherTaxaXPath = ".//p|.//td|.//th|.//li|.//article-title|.//title|.//label|.//ref|.//kwd|.//tp:nomenclature-citation|.//*[@object_id='95']|.//value[../@id!='244'][../@id!='434'][../@id!='433'][../@id!='432'][../@id!='431'][../@id!='430'][../@id!='429'][../@id!='428'][../@id!='427'][../@id!='426'][../@id!='425'][../@id!='424'][../@id!='423'][../@id!='422'][../@id!='421'][../@id!='420'][../@id!='419'][../@id!='417'][../@id!='48']";

        private readonly IHigherTaxaDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly IPersonNamesHarvester personNamesHarvester;
        private readonly IBlackList blacklist;
        private readonly IWhiteList whitelist;
        private readonly IStringTagger contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HigherTaxaTagger"/> class.
        /// </summary>
        /// <param name="miner">Higher taxa data miner.</param>
        /// <param name="contentHarvester">Content harvester.</param>
        /// <param name="personNamesHarvester">Person names harvester.</param>
        /// <param name="blacklist">Taxonomic black list.</param>
        /// <param name="whitelist">Taxonomic white list.</param>
        /// <param name="contentTagger">Content tagger.</param>
        public HigherTaxaTagger(IHigherTaxaDataMiner miner, ITextContentHarvester contentHarvester, IPersonNamesHarvester personNamesHarvester, IBlackList blacklist, IWhiteList whitelist, IStringTagger contentTagger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.personNamesHarvester = personNamesHarvester ?? throw new ArgumentNullException(nameof(personNamesHarvester));
            this.blacklist = blacklist ?? throw new ArgumentNullException(nameof(blacklist));
            this.whitelist = whitelist ?? throw new ArgumentNullException(nameof(whitelist));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        /// <inheritdoc/>
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
            await this.contentTagger.TagAsync(context, taxaNames, tagModel, HigherTaxaXPath).ConfigureAwait(false);

            return true;
        }

        private async Task<string[]> GetStopWords(XmlNode context)
        {
            var personNames = await this.personNamesHarvester.HarvestAsync(context).ConfigureAwait(false);
            var blacklistItems = await this.blacklist.GetItemsAsync().ConfigureAwait(false);

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
