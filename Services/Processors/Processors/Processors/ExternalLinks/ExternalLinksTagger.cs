﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ProcessingTools.Attributes.Extensions;
using ProcessingTools.Contracts;
using ProcessingTools.Data.Miners.Contracts.Miners.ExternalLinks;
using ProcessingTools.Harvesters.Contracts.Content;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Layout.Processors.Models.Taggers;
using ProcessingTools.Processors.Contracts.ExternalLinks;
using ProcessingTools.Processors.Models.ExternalLinks;

namespace ProcessingTools.Processors.Processors.ExternalLinks
{
    public class ExternalLinksTagger : IExternalLinksTagger
    {
        private const string XPath = "./*";

        private readonly IExternalLinksDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel> contentTagger;
        private readonly ILogger logger;

        public ExternalLinksTagger(
            IExternalLinksDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel> contentTagger,
            ILogger logger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.miner = miner;
            this.contentHarvester = contentHarvester;
            this.contentTagger = contentTagger;
            this.logger = logger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    Href = i.Href,
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true
            };

            await this.contentTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, settings);

            return true;
        }
    }
}
