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
    using ProcessingTools.Xml.Extensions;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : TaggerControllerFactory, ITagEnvironmentTermsWithExtractController
    {
        private const string XPath = "/*";
        private readonly IExtractHcmrDataMiner miner;

        public TagEnvironmentTermsWithExtractController(IDocumentFactory documentFactory, IExtractHcmrDataMiner miner)
            : base(documentFactory)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel>(document.Xml, data, XPath, document.NamespaceManager, false, true, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}