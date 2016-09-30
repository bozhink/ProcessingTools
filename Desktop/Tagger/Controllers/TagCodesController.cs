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
        private readonly IBiorepositoriesInstitutionsDataMiner institutionsMiner;
        private readonly IBiorepositoriesCollectionsDataMiner collectionsMiner;

        public TagCodesController(IBiorepositoriesInstitutionsDataMiner institutionsMiner, IBiorepositoriesCollectionsDataMiner collectionsMiner)
        {
            if (institutionsMiner == null)
            {
                throw new ArgumentNullException(nameof(institutionsMiner));
            }

            if (collectionsMiner == null)
            {
                throw new ArgumentNullException(nameof(collectionsMiner));
            }

            this.institutionsMiner = institutionsMiner;
            this.collectionsMiner = collectionsMiner;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var textContent = document.XmlDocument.GetTextContent();
            await this.TagInstitutions(document.XmlDocument, document.NamespaceManager, logger, textContent);
            ////await this.TagCollections(document, namespaceManager, logger, textContent);
        }

        private async Task TagInstitutions(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, string textContent)
        {
            var data = await this.institutionsMiner.Mine(textContent);

            await this.TagInstitutionalCodes(document, namespaceManager, logger, data);
            await this.TagInstitutions(document, namespaceManager, logger, data);
        }

        private async Task TagInstitutionalCodes(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoriesInstitutionalCodeSerializableModel
            {
                Description = i.NameOfInstitution,
                Url = i.Url,
                Value = i.InstitutionalCode
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel>(document.OuterXml, institutionalCodes, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }

        private async Task TagInstitutions(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutions = data.Select(i => new BiorepositoriesInstitutionSerializableModel
            {
                Url = i.Url,
                Value = i.NameOfInstitution
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel>(document.OuterXml, institutions, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }

        private async Task TagCollections(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, string textContent)
        {
            var data = await this.collectionsMiner.Mine(textContent);

            await this.TagCollectionCodes(document, namespaceManager, logger, data);
            await this.TagCollections(document, namespaceManager, logger, data);
        }

        private async Task TagCollectionCodes(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesCollection> data)
        {
            var collectionCodes = data.Select(c => new BiorepositoriesCollectionCodeSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionCode,
                XLinkTitle = c.CollectionName
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel>(document.OuterXml, collectionCodes, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }

        private async Task TagCollections(XmlDocument document, XmlNamespaceManager namespaceManager, ILogger logger, IQueryable<Bio.Data.Miners.Models.BiorepositoriesCollection> data)
        {
            var collections = data.Select(c => new BiorepositoriesCollectionSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionName
            });

            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel>(document.OuterXml, collections, XPath, namespaceManager, true, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
