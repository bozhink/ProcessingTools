namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    [Description("Tag codes.")]
    public class TagCodesController : TaggerControllerFactory, ITagCodesController
    {
        private const string XPath = "/*";
        private IBiorepositoriesInstitutionsDataMiner miner;

        public TagCodesController(IBiorepositoriesInstitutionsDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var textContent = document.GetTextContent();
            var data = await this.miner.Mine(textContent);

            await this.TagInstitutionalCodes(document, namespaceManager, logger, data);
            await this.TagInstitutions(document, namespaceManager, logger, data);
        }

        private async Task TagInstitutionalCodes(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoryInstitutionalCodeSerializableModel
            {
                Description = i.NameOfInstitution,
                Url = i.Url,
                Value = i.InstitutionalCode
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoryInstitutionalCodeSerializableModel>(document.OuterXml, institutionalCodes, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }

        private async Task TagInstitutions(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoryInstitutionSerializableModel
            {
                Url = i.Url,
                Value = i.NameOfInstitution
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoryInstitutionSerializableModel>(document.OuterXml, institutionalCodes, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
