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

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsController : TaggerControllerFactory, ITagEnvironmentTermsController
    {
        private const string XPath = "/*";
        private readonly IEnvoTermsDataMiner miner;

        public TagEnvironmentTermsController(IEnvoTermsDataMiner miner)
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

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoTermSerializableModel>(document.Xml, data, XPath, document.NamespaceManager, false, true, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}