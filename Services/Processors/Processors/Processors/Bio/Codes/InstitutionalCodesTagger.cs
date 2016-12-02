namespace ProcessingTools.Processors.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Codes;
    using Models.Bio.Codes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;

    public class InstitutionalCodesTagger : IInstitutionalCodesTagger
    {
        private const string XPath = "./*";
        private readonly ITextContentHarvester contentHarvester;
        private readonly IBiorepositoriesInstitutionsDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel> institutionalCodesTagger;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel> institutionsTagger;

        public InstitutionalCodesTagger(
            ITextContentHarvester contentHarvester,
            IBiorepositoriesInstitutionsDataMiner miner,
            ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel> institutionalCodesTagger,
            ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel> institutionsTagger)
        {
            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (institutionalCodesTagger == null)
            {
                throw new ArgumentNullException(nameof(institutionalCodesTagger));
            }

            if (institutionsTagger == null)
            {
                throw new ArgumentNullException(nameof(institutionsTagger));
            }

            this.contentHarvester = contentHarvester;
            this.miner = miner;
            this.institutionalCodesTagger = institutionalCodesTagger;
            this.institutionsTagger = institutionsTagger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .ToArray();

            await this.TagInstitutionalCodes(document, data);
            await this.TagInstitutions(document, data);

            return true;
        }

        private async Task TagInstitutionalCodes(
            IDocument document,
            IEnumerable<IBiorepositoriesInstitution> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoriesInstitutionalCodeSerializableModel
            {
                Description = i.NameOfInstitution,
                Url = i.Url,
                Value = i.InstitutionalCode
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true
            };

            await this.institutionalCodesTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, institutionalCodes, XPath, settings);
        }

        private async Task TagInstitutions(
            IDocument document,
            IEnumerable<IBiorepositoriesInstitution> data)
        {
            var institutions = data.Select(i => new BiorepositoriesInstitutionSerializableModel
            {
                Url = i.Url,
                Value = i.NameOfInstitution
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true
            };

            await this.institutionsTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, institutions, XPath, settings);
        }
    }
}
