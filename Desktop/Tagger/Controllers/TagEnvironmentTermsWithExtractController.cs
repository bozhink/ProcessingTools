namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Taggers;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Extensions;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : TaggerControllerFactory, ITagEnvironmentTermsWithExtractController
    {
        private const string XPath = "/*";
        private readonly IExtractHcmrDataMiner miner;
        private readonly ILogger logger;

        public TagEnvironmentTermsWithExtractController(
            IDocumentFactory documentFactory,
            IExtractHcmrDataMiner miner,
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
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel>(new XmlSerializer<EnvoExtractHcmrSerializableModel>(), this.logger);

            await tagger.Tag(document.XmlDocument, document.NamespaceManager, data, XPath, false, true);
        }
    }
}
