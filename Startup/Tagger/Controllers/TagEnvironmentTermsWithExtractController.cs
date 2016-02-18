namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using Models;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;
    using ProcessingTools.Infrastructure.Extensions;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : TaggerControllerFactory, ITagEnvironmentTermsWithExtractController
    {
        private const string XPath = "/*";
        private readonly IExtractHcmrDataMiner miner;

        public TagEnvironmentTermsWithExtractController(IExtractHcmrDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException("miner");
            }

            this.miner = miner;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var textContent = document.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel>(document.OuterXml, data, XPath, namespaceManager, false, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}