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
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Taggers;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Extensions;

    [Description("Tag codes.")]
    public class TagCodesController : TaggerControllerFactory, ITagCodesController
    {
        private const string XPath = "/*";
        private readonly IBiorepositoriesInstitutionsDataMiner institutionsMiner;
        private readonly IBiorepositoriesCollectionsDataMiner collectionsMiner;
        private readonly ILogger logger;

        public TagCodesController(
            IDocumentFactory documentFactory,
            IBiorepositoriesInstitutionsDataMiner institutionsMiner,
            IBiorepositoriesCollectionsDataMiner collectionsMiner,
            ILogger logger)
            : base(documentFactory)
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
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var textContent = document.XmlDocument.GetTextContent();
            await this.TagInstitutions(document.XmlDocument, document.NamespaceManager, textContent);
            ////await this.TagCollections(document, namespaceManager, logger, textContent);
        }

        private async Task TagInstitutions(XmlDocument document, XmlNamespaceManager namespaceManager, string textContent)
        {
            var data = await this.institutionsMiner.Mine(textContent);

            await this.TagInstitutionalCodes(document, namespaceManager, data);
            await this.TagInstitutions(document, namespaceManager, data);
        }

        private async Task TagInstitutionalCodes(XmlDocument document, XmlNamespaceManager namespaceManager, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoriesInstitutionalCodeSerializableModel
            {
                Description = i.NameOfInstitution,
                Url = i.Url,
                Value = i.InstitutionalCode
            });

            // TODO: DI
            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel>(new XmlSerializer<BiorepositoriesInstitutionalCodeSerializableModel>(), this.logger);

            await tagger.Tag(document.DocumentElement, namespaceManager, institutionalCodes, XPath, true, true);
        }

        private async Task TagInstitutions(XmlDocument document, XmlNamespaceManager namespaceManager, IQueryable<Bio.Data.Miners.Models.BiorepositoriesInstitution> data)
        {
            var institutions = data.Select(i => new BiorepositoriesInstitutionSerializableModel
            {
                Url = i.Url,
                Value = i.NameOfInstitution
            });

            // TODO: DI
            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel>(new XmlSerializer<BiorepositoriesInstitutionSerializableModel>(), this.logger);

            await tagger.Tag(document.DocumentElement, namespaceManager, institutions, XPath, true, true);
        }

        private async Task TagCollections(XmlDocument document, XmlNamespaceManager namespaceManager, string textContent)
        {
            var data = await this.collectionsMiner.Mine(textContent);

            await this.TagCollectionCodes(document, namespaceManager, data);
            await this.TagCollections(document, namespaceManager, data);
        }

        private async Task TagCollectionCodes(XmlDocument document, XmlNamespaceManager namespaceManager, IQueryable<Bio.Data.Miners.Models.BiorepositoriesCollection> data)
        {
            var collectionCodes = data.Select(c => new BiorepositoriesCollectionCodeSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionCode,
                XLinkTitle = c.CollectionName
            });

            // TODO: DI
            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel>(new XmlSerializer<BiorepositoriesCollectionCodeSerializableModel>(), this.logger);

            await tagger.Tag(document.DocumentElement, namespaceManager, collectionCodes, XPath, true, true);
        }

        private async Task TagCollections(XmlDocument document, XmlNamespaceManager namespaceManager, IQueryable<Bio.Data.Miners.Models.BiorepositoriesCollection> data)
        {
            var collections = data.Select(c => new BiorepositoriesCollectionSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionName
            });

            // TODO: DI
            var tagger = new SimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel>(new XmlSerializer<BiorepositoriesCollectionSerializableModel>(), this.logger);

            await tagger.Tag(document.DocumentElement, namespaceManager, collections, XPath, true, true);
        }
    }
}
