namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Extensions;

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsController : TaggerControllerFactory, ITagEnvironmentTermsController
    {
        private const string XPath = "/*";
        private readonly IEnvoTermsDataMiner miner;
        private readonly ILogger logger;

        public TagEnvironmentTermsController(
            IDocumentFactory documentFactory,
            IEnvoTermsDataMiner miner,
            ILogger logger)
            : base(documentFactory)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(t => new EnvoTermResponseModel
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .Select(t => new EnvoTermSerializableModel
                {
                    Value = t.Content,
                    EnvoId = t.EnvoId,
                    Id = t.EntityId,
                    VerbatimTerm = t.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoTermSerializableModel>(new XmlSerializer<EnvoTermSerializableModel>(), this.logger);

            await tagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, false, true);
        }
    }
}
