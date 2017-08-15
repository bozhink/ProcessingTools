namespace ProcessingTools.Processors.Processors.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Codes;
    using ProcessingTools.Processors.Models.Bio.Codes;

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
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.institutionalCodesTagger = institutionalCodesTagger ?? throw new ArgumentNullException(nameof(institutionalCodesTagger));
            this.institutionsTagger = institutionsTagger ?? throw new ArgumentNullException(nameof(institutionsTagger));
        }

        public async Task<object> Tag(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var textContent = await this.contentHarvester.Harvest(context.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .ToArray();

            await this.TagInstitutionalCodes(context, data);
            await this.TagInstitutions(context, data);

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
